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
    public class SuppliersServiceTests
    {
        [Test()]
        public void GetAllSuppliers_Should_Return_Suppliers()
        {
            //Arrange
            string binPath = Path.GetDirectoryName((new System.Uri(Assembly.GetExecutingAssembly().CodeBase)).LocalPath);
            var suppliersJsonPath = binPath + @"\Data\suppliers.json";

            var jsonService = new JsonService();
            var suppliersService = new SuppliersService(jsonService);
            
            //Act
            var suppliers = suppliersService.GetAllSuppliers(suppliersJsonPath);
            //Assert
            Assert.IsTrue(suppliers.Count > 0);
            Assert.IsInstanceOf(typeof(Supplier), suppliers[0]);
        }
    }
}
