using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomPromotionEngine.Models
{
    public class Items
    {
        public char Sku_Id { get; set; }
        public int Quantity { get; set; }
        public Items()
        {
        }
        public Items(Items items)
        {
            Sku_Id = items.Sku_Id;
            Quantity = items.Quantity;
        }
        public override string ToString()
        {
            return $"{Sku_Id} {Quantity}";
        }
    }
}
