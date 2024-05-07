using Xunit;
using System.Collections.Generic;
using GildedRoseKata;

namespace GildedRoseTests
{
    public class GildedRoseTest
    {
        [Theory]
        [InlineData("stuff", 1, 10, 0, 9)]
        [InlineData("stuff", 2, 10, 1, 9)]
        [InlineData("stuff", 0, 10, -1, 8)]
        [InlineData(KeyItem.AgedBrie, 5, 10, 4, 11)]
        [InlineData(KeyItem.AgedBrie, 1, 10, 0, 11)]
        [InlineData(KeyItem.AgedBrie, 0, 10, -1, 12)]
        [InlineData(KeyItem.AgedBrie, 5, 50, 4, 50)]
        [InlineData(KeyItem.BackstagePass, 11, 10, 10, 11)]
        [InlineData(KeyItem.BackstagePass, 10, 10, 9, 12)]
        [InlineData(KeyItem.BackstagePass, 6, 10, 5, 12)]
        [InlineData(KeyItem.BackstagePass, 5, 10, 4, 13)]
        [InlineData(KeyItem.BackstagePass, 5, 50, 4, 50)]
        [InlineData(KeyItem.BackstagePass, 4, 10, 3, 13)]
        [InlineData(KeyItem.BackstagePass, 4, 50, 3, 50)]
        [InlineData(KeyItem.BackstagePass, 1, 10, 0, 13)]
        [InlineData(KeyItem.BackstagePass, 0, 10, -1, 0)]
        [InlineData(KeyItem.Sulfuras, 10, 80, 10, 80)]
        [InlineData(KeyItem.Sulfuras, 10, 10, 10, 10)]
        [InlineData(KeyItem.Sulfuras, 20, 20, 20, 20)]
        [InlineData(KeyItem.Sulfuras, 0, 20, 0, 20)]
        [InlineData(KeyItem.Sulfuras, -1, 80, -1, 80)]
        public void GivenNameSellInAndQuality_TheGildedRose_ShouldCalculateTheRightSellInAndQuality(string name, int sellIn, int quality, int expectedSellIn, int expectedQuality)
        {
            IList<Item> Items = new List<Item> { new Item { Name = name, SellIn = sellIn, Quality = quality } };
            GildedRose app = new GildedRose(Items);
            app.UpdateQuality();
            Assert.Equal(expectedSellIn, Items[0].SellIn);
            Assert.Equal(expectedQuality, Items[0].Quality);
        }
    }
}
