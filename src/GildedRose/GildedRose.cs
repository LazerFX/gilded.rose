using System.Collections.Generic;

namespace GildedRoseKata
{
    public static class KeyValues
    {
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
                if (item.Name != KeyValues.AgedBrie && item.Name != KeyValues.BackstagePass.Name)
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

                    if (item.Name == KeyValues.BackstagePass.Name)
                    {
                        item.Quality = KeyValues.BackstagePass.OverageValue;
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
