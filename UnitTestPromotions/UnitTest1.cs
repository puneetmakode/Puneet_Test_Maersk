using System;
using System.Collections.Generic;
using CustomPromotionEngine.Models;
using CustomPromotionEngine.MyEngine;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestPromotions
{
    [TestClass]
    public class UnitTest1
    {
        static readonly IEnumerable<SkuPrices> PriceList =
      new List<SkuPrices> {
        new SkuPrices { Sku_Id = 'A', UnitPrice = 50 },
        new SkuPrices { Sku_Id = 'B', UnitPrice = 30 },
        new SkuPrices { Sku_Id = 'C', UnitPrice = 20 },
        new SkuPrices { Sku_Id = 'D', UnitPrice = 15 } };

        static readonly IEnumerable<Promotions> Promotions =
          new List<Promotions> {
        new Promotions {
          Items = new List<Items> {
            new Items { Sku_Id = 'A', Quantity = 3 }},
          TotalAmount = 130 },
        new Promotions {
          Items = new List<Items> {
            new Items { Sku_Id = 'B', Quantity = 2 }},
          TotalAmount = 45 },
        new Promotions {
          Items = new List<Items> {
            new Items { Sku_Id = 'C', Quantity = 1 },
            new Items { Sku_Id = 'D', Quantity = 1 }},
          TotalAmount = 30 } };
        static readonly PromotionEngine actualEngine = new PromotionEngine(PriceList, Promotions);

        [TestMethod]
        public void Test_Scenario_CaseOne()
        {
            var order =
              new Orders
              {
                  Items = new List<Items>
                {
            new Items { Sku_Id = 'A', Quantity = 1 },
            new Items { Sku_Id = 'B', Quantity = 1 },
            new Items { Sku_Id = 'C', Quantity = 1 }}
              };

            actualEngine.CheckOut(order);
            Assert.IsTrue(order.TotalAmount == 100);
        }

        [TestMethod]
        public void Test_Scenario_CaseTwo()
        {
            var order =
              new Orders
              {
                  Items = new List<Items>
                {
            new Items { Sku_Id = 'A', Quantity = 5 },
            new Items { Sku_Id = 'B', Quantity = 5 },
            new Items { Sku_Id = 'C', Quantity = 1 }}
              };

            actualEngine.CheckOut(order);
            Assert.IsTrue(order.TotalAmount == 370);
        }

        [TestMethod]
        public void Test_Scenario_CaseThree()
        {
            var order =
              new Orders
              {
                  Items = new List<Items>
                {
            new Items { Sku_Id = 'A', Quantity = 3 },
            new Items { Sku_Id = 'B', Quantity = 5 },
            new Items { Sku_Id = 'C', Quantity = 1 },
            new Items { Sku_Id = 'D', Quantity = 1 }}
              };

            actualEngine.CheckOut(order);
            Assert.IsTrue(order.TotalAmount == 280);
        }
    }
}
