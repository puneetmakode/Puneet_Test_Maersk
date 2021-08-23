using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomPromotionEngine.Models
{
    public class Promotions: Orders
    {
        public IEnumerable<Items> Validate(Orders order, IEnumerable<Items> validatedItems)
        {
            var foundItems = new List<Items>();
            if (Items == null || Items.Count < 1)
                return foundItems;

            foreach (var promotionItem in Items)
            {
                var foundItem = validatedItems.FirstOrDefault(x => x.Sku_Id == promotionItem.Sku_Id) ??
                  order.Items.FirstOrDefault(x => x.Sku_Id == promotionItem.Sku_Id);
                if (foundItem == null || foundItem.Quantity < promotionItem.Quantity)
                    return null;

                if (!foundItems.Any(x => x.Sku_Id == foundItem.Sku_Id))
                    foundItems.Add(new Items(foundItem));
            }

            ApplyPromotionPriceAndCalculateRestQuantity(order, foundItems);

            return foundItems;
        }
        private void ApplyPromotionPriceAndCalculateRestQuantity(Orders order, List<Items> foundItems)
        {
            var found = foundItems.Count() > 0;
            if (found)
            {
                do
                {
                    order.TotalAmount += TotalAmount;
                    foreach (var promotionItem in Items)
                    {
                        var item = foundItems.FirstOrDefault(x => x.Sku_Id == promotionItem.Sku_Id);
                        if (item != null)
                        {
                            item.Quantity -= promotionItem.Quantity;
                            if (found)
                                found = item.Quantity >= promotionItem.Quantity;
                        }
                    }
                } while (found);
            }
        }
    }
}
