using OrderSystem.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderSystem.Application.Interfaces
{
    public interface ISuppliersService
    {
        List<Supplier> GetAllSuppliers(string path ="");
        List<Supplier> GetAvailableSuppliers();
    }
}
