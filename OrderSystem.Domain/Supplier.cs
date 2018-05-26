using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace OrderSystem.Domain
{
    /// <summary>
    /// Waffle Supplier
    /// </summary>
    public class Supplier
    {
        #region Properites
        public string Name { get; set; }

        [JsonProperty("Products")]
        public Products WaffleProducts { get; set; }

        public bool IsServiceOfflineOnPublicHolidays { get; set; }

        #region Shipping
        public decimal ShippingConstant { get; set; }
        public int ShippingPercentage { get; set; }
        public decimal FreeDeliveryIfExceeds { get; set; }
        #endregion

        /// <summary>
        /// Total cost with shipping costs (if there is no free delivery)
        /// </summary>
        public decimal TotalCost { get; set; }

        [JsonExtensionData]
        private IDictionary<string, JToken> _additionalData;

        #endregion

        #region Constructors
        public Supplier()
        {
            _additionalData = new Dictionary<string, JToken>();
        }
        #endregion

        /// <summary>
        /// Handle Supplier C - Dutch company 
        /// </summary>
        /// <param name="context"></param>
        [OnDeserialized]
        private void OnDeserialized(StreamingContext context)
        {
            // Producten is not deserialized to any property
            // and so it is added to the extension data dictionary
            if (_additionalData.Keys.Count > 0)
            {
                var Producten = _additionalData["producten"];
                WaffleProducts = new Products
                {
                    Ordinary = Producten.Value<decimal?>("gewone") ?? 0,
                    SugarFree = Producten.Value<decimal?>("suikervrije") ?? 0,
                    Super = Producten.Value<decimal?>("super") ?? 0
                };
            }
        }
        
    }
}
