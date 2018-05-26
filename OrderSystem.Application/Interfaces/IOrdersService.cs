using OrderSystem.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderSystem.Application.Interfaces
{
    public interface IOrdersService
    {
        List<Order> GetOrders(string path = "");
        void SaveOrders(List<Order> orders);
        Order CreateOrder(Order order);
        Supplier FindCheapestSupplier(Order order);
    }
}
