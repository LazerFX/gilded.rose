using System.Collections.Generic;

namespace GildedRoseKata
{
    public static class KeyValues
    {
        public const string AgedBrie = "Aged Brie";
        public const string BackstagePass = "Backstage passes to a TAFKAL80ETC concert";
        public const int BackstagePassOverageValue = 0;
        public const int BackstagePassFirstQualityIncrease = 10;
        public const int BackstagePassFirstQualityIncreaseValue = 2;
        public const int BackstagePassSecondQualityIncrease = 5;
        public const int BackstagePassSecondQualityIncreaseValue = 3;
        public const string Sulfuras = "Sulfuras, Hand of Ragnaros";
        public const int MaxQuality = 50;
    }

    public class GildedRose
    {

        IList<Item> Items;
        public GildedRose(IList<Item> Items)
        {
            this.Items = Items;
        }

        public void UpdateQuality()
        {
            foreach (var item in Items)
            {
                if (item.Name != KeyValues.AgedBrie && item.Name != KeyValues.BackstagePass)
                {
                    if (item.Quality > 0)
                    {
                        if (item.Name != KeyValues.Sulfuras)
                        {
                            item.Quality--;
                        }
                    }
                }

                if (item.Name == KeyValues.AgedBrie)
                {
                    if (item.Quality < KeyValues.MaxQuality)
                    {
                        item.Quality++;
                    }
                }

                if (item.Name == KeyValues.BackstagePass)
                {
                    if (item.SellIn > KeyValues.BackstagePassFirstQualityIncreaseValue)
                    {
                        item.Quality++;
                    }
                    else if (item.SellIn <= KeyValues.BackstagePassFirstQualityIncrease &&
                            item.SellIn > KeyValues.BackstagePassSecondQualityIncrease)
                    {
                        item.Quality += KeyValues.BackstagePassFirstQualityIncrease;
                    }
                    else if (item.SellIn <= KeyValues.BackstagePassSecondQualityIncrease)
                    {
                        item.Quality += KeyValues.BackstagePassSecondQualityIncreaseValue;
                    }
                }

                if (item.Name != KeyValues.Sulfuras)
                {
                    item.SellIn--;
                }

                if (item.SellIn < 0)
                {
                    if (item.Name == KeyValues.AgedBrie && item.Quality < KeyValues.MaxQuality)
                    {
                        item.Quality++;
                    }

                    if (item.Name == KeyValues.BackstagePass)
                    {
                        item.Quality = KeyValues.BackstagePassOverageValue;
                    }

                    if (item.Quality > 0 && item.Name != KeyValues.Sulfuras)
                    {
                        item.Quality--;
                    }

                }
            }
        }
    }
}
