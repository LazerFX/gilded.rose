﻿using System.Collections.Generic;
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

        public const string ConjuredPrefix = "Conjured ";
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
                if (item.Name is null) {
                    continue;
                }

                if (KeyValues.KeyItems.Contains(item.Name) || item.Name.StartsWith(KeyValues.ConjuredPrefix))
                {
                    HandleKey(item);
                }
                else
                {
                    HandleStandard(item);
                }
            }
        }

        private static void HandleKey(Item item)
        {
            switch (item.Name)
            {
                case KeyValues.Sulfuras:
                    return;
                case KeyValues.AgedBrie:
                    HandleAgedBrie(item);
                    return;
                case KeyValues.BackstagePass.Name:
                    HandleBackstagePass(item);
                    return;
                case { } when item.Name.StartsWith(KeyValues.ConjuredPrefix):
                    HandleConjuredPrefix(item);
                    return;
            }
        }

        private static void HandleConjuredPrefix(Item item) {
            item.SellIn--;

            if (item.SellIn < 0 && item.Quality > 0)
            {
                item.Quality -= KeyValues.AgedQualityReduction * 2;
            }
            else
            {
                item.Quality = int.Max(item.Quality - KeyValues.QualityReduction * 2, 0);
            }

            item.Quality = int.Max(item.Quality, 0);
        }

        private static void HandleBackstagePass(Item item)
        {
            if (item.SellIn > KeyValues.BackstagePass.FirstSellInBoundary &&
                                    item.Quality < KeyValues.MaxQuality)
            {
                item.Quality++;
            }
            else if (item.SellIn <= KeyValues.BackstagePass.FirstSellInBoundary &&
                    item.SellIn > KeyValues.BackstagePass.SecondSellInBoundary &&
                    item.Quality < KeyValues.MaxQuality)
            {
                item.Quality += KeyValues.BackstagePass.FirstIncrease;
            }
            else if (item.SellIn <= KeyValues.BackstagePass.SecondSellInBoundary &&
                    item.Quality < KeyValues.MaxQuality)
            {
                item.Quality += KeyValues.BackstagePass.SecondIncrease;
            }

            item.Quality = int.Min(item.Quality, KeyValues.MaxQuality);
            item.SellIn--;
            if (item.SellIn < 0)
            {
                item.Quality = KeyValues.BackstagePass.OverageValue;
            }
            return;
        }

        private static void HandleAgedBrie(Item item)
        {
            if (item.Quality < KeyValues.MaxQuality)
            {
                item.Quality++;
            }

            item.SellIn--;

            if (item.SellIn < 0 && item.Quality < KeyValues.MaxQuality)
            {
                item.Quality++;
            }
            return;
        }

        private static void HandleStandard(Item item)
        {
            item.SellIn--;

            if (item.SellIn < 0 && item.Quality > 0)
            {
                item.Quality -= KeyValues.AgedQualityReduction;
            }
            else
            {
                item.Quality = int.Max(item.Quality - KeyValues.QualityReduction, 0);
            }

            item.Quality = int.Max(item.Quality, 0);
        }
    }
}
