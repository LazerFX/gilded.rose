using Xunit;
using System.Collections.Generic;
using GildedRoseKata;
using FluentAssertions;
using System;

namespace GildedRoseTests
{
    public class GildedRoseTest
    {
        [Theory]
        [InlineData("stuff", 1, 10, 0, 9)]
        [InlineData("stuff", 2, 10, 1, 9)]
        [InlineData("stuff", 0, 10, -1, 8)]
        [InlineData("stuff", -1, 0, -2, 0)]
        [InlineData("stuff", -1, -1, -2, 0)]
        [InlineData(KeyValues.AgedBrie, 5, 10, 4, 11)]
        [InlineData(KeyValues.AgedBrie, 1, 10, 0, 11)]
        [InlineData(KeyValues.AgedBrie, 0, 10, -1, 12)]
        [InlineData(KeyValues.AgedBrie, 5, KeyValues.MaxQuality, 4, KeyValues.MaxQuality)]
        [InlineData(KeyValues.AgedBrie, -1, 10, -2, 12)]
        [InlineData(KeyValues.AgedBrie, -1, KeyValues.MaxQuality, -2, KeyValues.MaxQuality)]
        [InlineData(KeyValues.AgedBrie, -1, 49, -2, KeyValues.MaxQuality)]
        [InlineData(KeyValues.AgedBrie, -1, 48, -2, KeyValues.MaxQuality)]
        [InlineData(KeyValues.BackstagePass.Name, 11, 10, 10, 11)]
        [InlineData(KeyValues.BackstagePass.Name, 10, 10, 9, 12)]
        [InlineData(KeyValues.BackstagePass.Name, 6, 10, 5, 12)]
        [InlineData(KeyValues.BackstagePass.Name, 5, 10, 4, 13)]
        [InlineData(KeyValues.BackstagePass.Name, 5, KeyValues.MaxQuality, 4, KeyValues.MaxQuality)]
        [InlineData(KeyValues.BackstagePass.Name, 5, 49, 4, KeyValues.MaxQuality)]
        [InlineData(KeyValues.BackstagePass.Name, 4, 10, 3, 13)]
        [InlineData(KeyValues.BackstagePass.Name, 4, KeyValues.MaxQuality, 3, KeyValues.MaxQuality)]
        [InlineData(KeyValues.BackstagePass.Name, 4, 48, 3, KeyValues.MaxQuality)]
        [InlineData(KeyValues.BackstagePass.Name, 1, 10, 0, 13)]
        [InlineData(KeyValues.BackstagePass.Name, 0, 10, -1, KeyValues.BackstagePass.OverageValue)]
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

            var expectedItem = new Item { Name = name, SellIn = expectedSellIn, Quality = expectedQuality };
            var actualItem = Items[0];
            actualItem.Should().BeEquivalentTo(expectedItem, $"{Environment.NewLine
                                        }Actual:   {actualItem.Name} - SI: {actualItem.SellIn}, Q: {actualItem.Quality}{Environment.NewLine
                                        }Expected: {expectedItem.Name} - SI: {expectedItem.SellIn}, Q: {expectedItem.Quality}");
        }
    }
}
