using NUnit.Framework;
using OrderSystem.Application.Services;
using OrderSystem.Domain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace OrderSystem.Application.Tests
{
    [TestFixture()]
    public class OrdersServiceTests
    {
        [Test()]
        public void GetOrders_Should_Return_Orders()
        {
            //Arrange
            string binPath = Path.GetDirectoryName((new System.Uri(Assembly.GetExecutingAssembly().CodeBase)).LocalPath);
            var ordersJsonPath = binPath + @"\Data\orders.json";

            var jsonService = new JsonService();
            var suppliersService = new SuppliersService(jsonService);
            var ordersService = new OrdersService(suppliersService, jsonService);

            //Act
            var orders = ordersService.GetOrders(ordersJsonPath);
            //Assert
            Assert.IsTrue(orders.Count > 0);
            Assert.IsInstanceOf(typeof(Order), orders[0]);
        }

        //Constant Shipping
        [Test()]
        public void CalculateTotalCost_WithConstantShipping_Should_Return_RightCost()
        {
            //Arrange
            var jsonService = new JsonService();
            var suppliersService = new SuppliersService(jsonService);
            var ordersService = new OrdersService(suppliersService, jsonService);

            var supplier = new Supplier
            {
                Name = "Supplier D",
                ShippingConstant = 10,
                WaffleProducts = new Products
                {
                    Ordinary = 2,
                    SugarFree = 2.5M,
                    Super = 2.8M
                }
            };
            var Order = new Order
            {
                OrdinaryCount = 2,
                SugarFreeCount = 3,
                SuperCount = 4
            };
            var expected = 29.9M;
            //Act
            var totalCost = ordersService.CalculateTotalCost(supplier, Order);

            //Assert
            Assert.IsTrue(totalCost == expected);
        }

        //Percentage Shipping
        [Test()]
        public void CalculateTotalCost_WithPercentageShipping_Should_Return_RightCost()
        {
            //Arrange
            var jsonService = new JsonService();
            var suppliersService = new SuppliersService(jsonService);
            var ordersService = new OrdersService(suppliersService, jsonService);

            var supplier = new Supplier
            {
                Name = "Supplier D",
                ShippingPercentage = 6,
                WaffleProducts = new Products
                {
                    Ordinary = 2,
                    SugarFree = 2.5M,
                    Super = 2.8M
                }
            };
            var Order = new Order
            {
                OrdinaryCount = 2,
                SugarFreeCount = 3,
                SuperCount = 4
            };
            var expected = 21.094M;
            //Act
            var totalCost = ordersService.CalculateTotalCost(supplier, Order);

            //Assert
            Assert.IsTrue(totalCost == expected);
        }
    }
}
