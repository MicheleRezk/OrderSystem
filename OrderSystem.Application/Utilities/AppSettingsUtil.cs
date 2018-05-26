using System.Configuration;

namespace OrderSystem.Application.Utilities
{
    /// <summary>
    /// Contains all the application settings
    /// </summary>
    public static class AppSettingsUtil
    {
        public static string OrdersJsonPath
        {
            get
            {
                return ConfigurationManager.AppSettings["OrdersJsonPath"];
            }
        }
        public static string SuppliersJsonPath
        {
            get
            {
                return ConfigurationManager.AppSettings["SuppliersJsonPath"];
            }
        }
    }
}
