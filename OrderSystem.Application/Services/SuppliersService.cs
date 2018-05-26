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
    public class SuppliersService : ISuppliersService
    {
        #region Fields
        private readonly IJsonService _JsonService = null;
        #endregion

        #region Constructors
        public SuppliersService(IJsonService jsonService)
        {
            _JsonService = jsonService;
        }
        #endregion

        #region Functions
        /// <summary>
        /// Get all waffles suppliers
        /// </summary>
        /// <param name="path">may take path of suppliers.json file</param>
        /// <returns>All Suppliers</returns>
        public List<Supplier> GetAllSuppliers(string path = "")
        {
            var suppliersJsonPath = path;
            if (string.IsNullOrEmpty(suppliersJsonPath))
                suppliersJsonPath = HttpContext.Current.Server.MapPath(AppSettingsUtil.SuppliersJsonPath);
            var suppliers = _JsonService.ReadJsonFile<List<Supplier>>(suppliersJsonPath);
            return suppliers;
        }

        /// <summary>
        /// Get only Available Suppliers, who has online Rest API
        /// Some suppliers has offline service on Sunday and Public Holidays
        /// </summary>
        /// <returns>Available Suppliers</returns>
        public List<Supplier> GetAvailableSuppliers()
        {
            var suppliers = GetAllSuppliers();
            //check if today is Sunday or Public Holiday in Netherlands, then return only available suppliers
            if (DateTime.Now.IsTodaySundayOrPublicHoliday())
            {
                suppliers = suppliers.Where(supplier => !supplier.IsServiceOfflineOnPublicHolidays).ToList();
            }
            return suppliers;
        }
        #endregion
    }
}
