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
    }
}
