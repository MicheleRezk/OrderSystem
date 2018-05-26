using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderSystem.Domain
{
    /// <summary>
    /// Waffle Products
    /// </summary>
    public class Products
    {
        #region Properites
        public decimal Ordinary { get; set; }
        [JsonProperty("Sugar-free")]
        public decimal SugarFree { get; set; }
        public decimal Super { get; set; }
        #endregion
    }
}
