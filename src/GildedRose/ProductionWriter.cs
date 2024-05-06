using System;

namespace GildedRoseKata
{
    public class ProductionWriter : IWriter
    {
        public void WriteLine(string? line)
        {
            Console.WriteLine(line);
        }
    }
}
