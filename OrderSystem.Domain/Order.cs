using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderSystem.Domain
{
    /// <summary>
    /// Waffles Order
    /// </summary>
    public class Order
    {
        public int OrdinaryCount { get; set; }
        public int SugarFreeCount { get; set; }
        public int SuperCount { get; set; }
        public DateTime OrderDate { get; set; }
        public Supplier BestSupplier { get; set; }
    }
}
