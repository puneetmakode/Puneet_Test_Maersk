using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomPromotionEngine.Models
{
    public class Orders
    {
        public List<Items> Items { get; set; }
        public double TotalAmount { get; set; }
    }
}
