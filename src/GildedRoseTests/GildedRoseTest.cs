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
        [InlineData(KeyValues.AgedBrie, 5, 10, 4, 11)]
        [InlineData(KeyValues.AgedBrie, 1, 10, 0, 11)]
        [InlineData(KeyValues.AgedBrie, 0, 10, -1, 12)]
        [InlineData(KeyValues.AgedBrie, 5, KeyValues.MaxQuality, 4, KeyValues.MaxQuality)]
        [InlineData(KeyValues.BackstagePass, 11, 10, 10, 11)]
        [InlineData(KeyValues.BackstagePass, 10, 10, 9, 12)]
        [InlineData(KeyValues.BackstagePass, 6, 10, 5, 12)]
        [InlineData(KeyValues.BackstagePass, 5, 10, 4, 13)]
        [InlineData(KeyValues.BackstagePass, 5, KeyValues.MaxQuality, 4, KeyValues.MaxQuality)]
        [InlineData(KeyValues.BackstagePass, 4, 10, 3, 13)]
        [InlineData(KeyValues.BackstagePass, 4, KeyValues.MaxQuality, 3, KeyValues.MaxQuality)]
        [InlineData(KeyValues.BackstagePass, 1, 10, 0, 13)]
        [InlineData(KeyValues.BackstagePass, 0, 10, -1, KeyValues.BackstagePassOverageValue)]
        [InlineData(KeyValues.Sulfuras, 10, 80, 10, 80)]
        [InlineData(KeyValues.Sulfuras, 10, 10, 10, 10)]
        [InlineData(KeyValues.Sulfuras, 20, 20, 20, 20)]
        [InlineData(KeyValues.Sulfuras, 0, 20, 0, 20)]
        [InlineData(KeyValues.Sulfuras, -1, 80, -1, 80)]
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
