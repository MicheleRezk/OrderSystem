using OrderSystem.Application.Interfaces;
using OrderSystem.Application.Utilities;
using OrderSystem.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace OrderSystem.Application.Services
{
    public class OrdersService : IOrdersService
    {
        #region Fields
        private readonly IJsonService _JsonService = null;
        private readonly ISuppliersService _SuppliersService = null;
        #endregion

        #region Constructors
        public OrdersService(ISuppliersService suppliersService, IJsonService jsonService)
        {
            _SuppliersService = suppliersService;
            _JsonService = jsonService;
        }
        #endregion

        #region Functions

        /// <summary>
        /// Find the cheapest Supplier and save this order
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public Order CreateOrder(Order order)
        {
            //set order Date
            order.OrderDate = DateTime.Now;

            //find the cheapest supplier and assign him to order
            var bestSupplier = FindCheapestSupplier(order);
            if (bestSupplier != null)
            {
                order.BestSupplier = bestSupplier;
                //get all orders and save this order
                var orders = GetOrders();
                orders.Add(order);
                SaveOrders(orders);
            }
            return order;
        }

        /// <summary>
        /// Find the cheapest Supplier
        /// </summary>
        /// <param name="order"></param>
        /// <returns>Cheapest Supplier</returns>
        public Supplier FindCheapestSupplier(Order order)
        {
            Supplier cheapestSupplier = null;
            var suppliers = _SuppliersService.GetAvailableSuppliers();
            if(suppliers.Count > 0)
            {
                //calculate total cost for each supplier
                foreach(var supplier in suppliers)
                {
                    supplier.TotalCost = CalculateTotalCost(supplier, order);
                }

                cheapestSupplier = suppliers.OrderBy(supplier => supplier.TotalCost).FirstOrDefault();
            }
            return cheapestSupplier;
        }

        /// <summary>
        /// Get All Waffle Orders
        /// </summary>
        /// <param name="path">may take path of orders.json file</param>
        /// <returns>Waffle Orders</returns>
        public List<Order> GetOrders(string path = "")
        {
            var ordersJsonPath = path;
            if (string.IsNullOrEmpty(ordersJsonPath))
                ordersJsonPath = HttpContext.Current.Server.MapPath(AppSettingsUtil.OrdersJsonPath);
            var orders = _JsonService.ReadJsonFile<List<Order>>(ordersJsonPath);
            return orders;
        }

        /// <summary>
        /// Save Orders into Json File
        /// </summary>
        /// <param name="orders"></param>
        public void SaveOrders(List<Order> orders)
        {
            var ordersJsonPath = HttpContext.Current.Server.MapPath(AppSettingsUtil.OrdersJsonPath);
            _JsonService.WriteJsonFile<List<Order>>(orders,ordersJsonPath);
        }


        #region HelperFunctions
        /// <summary>
        /// Calculate Total Cost with Shipping Costs (if there is no free delivery)
        /// </summary>
        /// <param name="supplier"></param>
        /// <param name="order"></param>
        /// <returns>Total Cost</returns>
        public decimal CalculateTotalCost(Supplier supplier, Order order)
        {
            decimal totalCost = 0;
            decimal shippingCost = 0;
            decimal total = 0;

            //calculate totalCost first
            totalCost = (order.OrdinaryCount * supplier.WaffleProducts.Ordinary)
                        + (order.SugarFreeCount * supplier.WaffleProducts.SugarFree)
                        + (order.SugarFreeCount * supplier.WaffleProducts.Super);

            //then calculate shipping costs
            if (supplier.ShippingConstant > 0)//Constant shipping
                shippingCost = supplier.ShippingConstant;
            else if(supplier.ShippingPercentage > 0)//Percentage shipping
                shippingCost = (totalCost * supplier.ShippingPercentage)/100;

            //calculate total
            //check if there is free delivery if orders exceed specific number
            if (supplier.FreeDeliveryIfExceeds == 0 || totalCost <= supplier.FreeDeliveryIfExceeds)
                total = totalCost + shippingCost;
            else//Free Delivery
                total = totalCost;

            return total;
        }
        #endregion

        #endregion

    }
}
