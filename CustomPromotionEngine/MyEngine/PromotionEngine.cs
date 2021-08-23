using CustomPromotionEngine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomPromotionEngine.MyEngine
{
    public class PromotionEngine
    {
        private IEnumerable<SkuPrices> PriceList;
        private IEnumerable<Promotions> Promotions;

        public PromotionEngine(IEnumerable<SkuPrices> priceList, IEnumerable<Promotions> promotions)
        {
            this.PriceList = priceList;
            this.Promotions = promotions;
        }

        public void CheckOut(Orders order)
        {
            var foundItems = new List<Items>();
            if (Promotions != null && Promotions.Count() > 0)
                foreach (var promotion in Promotions)
                {
                    var validatedItems = promotion.Validate(order, foundItems);
                    UpdateValidatedItems(foundItems, validatedItems);
                }

            ApplyRegularPrice(order, foundItems);
        }

        private void ApplyRegularPrice(Orders order, List<Items> foundItems)
        {
            foreach (var item in order.Items)
            {
                var validateItem = foundItems.FirstOrDefault(x => x.Sku_Id == item.Sku_Id) ?? item;
                var quantity = validateItem.Quantity;
                if (quantity > 0)
                    order.TotalAmount += quantity * PriceList.First(x => x.Sku_Id == item.Sku_Id).UnitPrice;
            }
        }

        private static void UpdateValidatedItems(List<Items> foundItems, IEnumerable<Items> validatedItems)
        {
            if (validatedItems == null || validatedItems.Count() < 1)
                return;

            foreach (var item in validatedItems)
                if (!foundItems.Any(x => x.Sku_Id == item.Sku_Id))
                    foundItems.Add(item);
        }
    }
}
