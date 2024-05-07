using System.Collections.Generic;

namespace GildedRoseKata
{
    public static class KeyValues
    {
        public const string AgedBrie = "Aged Brie";
        public const string BackstagePass = "Backstage passes to a TAFKAL80ETC concert";
        public const int BackstagePassOverageValue = 0;
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
            for (var i = 0; i < Items.Count; i++)
            {
                if (Items[i].Name != KeyValues.AgedBrie && Items[i].Name != KeyValues.BackstagePass)
                {
                    if (Items[i].Quality > 0)
                    {
                        if (Items[i].Name != KeyValues.Sulfuras)
                        {
                            Items[i].Quality = Items[i].Quality - 1;
                        }
                    }
                }
                else
                {
                    if (Items[i].Quality < KeyValues.MaxQuality)
                    {
                        Items[i].Quality = Items[i].Quality + 1;

                        if (Items[i].Name == KeyValues.BackstagePass)
                        {
                            if (Items[i].SellIn < 11)
                            {
                                if (Items[i].Quality < KeyValues.MaxQuality)
                                {
                                    Items[i].Quality = Items[i].Quality + 1;
                                }
                            }

                            if (Items[i].SellIn < 6)
                            {
                                if (Items[i].Quality < KeyValues.MaxQuality)
                                {
                                    Items[i].Quality = Items[i].Quality + 1;
                                }
                            }
                        }
                    }
                }

                if (Items[i].Name != KeyValues.Sulfuras)
                {
                    Items[i].SellIn = Items[i].SellIn - 1;
                }

                if (Items[i].SellIn < 0)
                {
                    if (Items[i].Name == KeyValues.AgedBrie && Items[i].Quality < KeyValues.MaxQuality)
                    {
                        Items[i].Quality = Items[i].Quality + 1;
                    }

                    if (Items[i].Name == KeyValues.BackstagePass)
                    {
                        Items[i].Quality = KeyValues.BackstagePassOverageValue;
                    }

                    if (Items[i].Quality > 0 && Items[i].Name != KeyValues.Sulfuras)
                    {
                        Items[i].Quality = Items[i].Quality - 1;
                    }

                }
            }
        }
    }
}
