using System.Collections.Generic;
using System.Linq;

namespace GildedRoseKata
{
    public static class KeyValues
    {
        public const int QualityReduction = 1;
        public const int AgedQualityReduction = 2;

        public static readonly string[] KeyItems = [AgedBrie, BackstagePass.Name, Sulfuras];

        public const string AgedBrie = "Aged Brie";

        public static class BackstagePass
        {
            public const string Name = "Backstage passes to a TAFKAL80ETC concert";
            public const int OverageValue = 0;
            public const int FirstSellInBoundary = 10;
            public const int FirstIncrease = 2;
            public const int SecondSellInBoundary = 5;
            public const int SecondIncrease = 3;
        }

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
                if (KeyValues.KeyItems.Contains(item.Name))
                {
                    if (item.Name == KeyValues.Sulfuras)
                    {
                        break;
                    }

                    if (item.Name != KeyValues.AgedBrie && item.Name != KeyValues.BackstagePass.Name && item.Quality > 0)
                    {
                        item.Quality--;
                    }

                    if (item.Name == KeyValues.AgedBrie && item.Quality < KeyValues.MaxQuality)
                    {
                        item.Quality++;
                    }

                    if (item.Name == KeyValues.BackstagePass.Name)
                    {
                        if (item.SellIn > KeyValues.BackstagePass.FirstSellInBoundary)
                        {
                            item.Quality++;
                        }
                        else if (item.SellIn <= KeyValues.BackstagePass.FirstSellInBoundary &&
                                item.SellIn > KeyValues.BackstagePass.SecondSellInBoundary)
                        {
                            item.Quality += KeyValues.BackstagePass.FirstIncrease;
                        }
                        else if (item.SellIn <= KeyValues.BackstagePass.SecondSellInBoundary)
                        {
                            item.Quality += KeyValues.BackstagePass.SecondIncrease;
                        }
                    }

                    if (item.SellIn < 0)
                    {
                        if (item.Name == KeyValues.AgedBrie && item.Quality < KeyValues.MaxQuality)
                        {
                            item.Quality++;
                        }

                        if (item.Name == KeyValues.BackstagePass.Name)
                        {
                            item.Quality = KeyValues.BackstagePass.OverageValue;
                        }

                        if (item.Quality > 0)
                        {
                            item.Quality--;
                        }
                    }
                    else
                    {
                        item.SellIn--;

                        if (item.SellIn < 0 && item.Quality > 0) {
                            item.Quality -= KeyValues.AgedQualityReduction;
                        }
                        else {
                            item.Quality -= KeyValues.QualityReduction;
                        }
                    }

                }
            }
        }
    }
}
