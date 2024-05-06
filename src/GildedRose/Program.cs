using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace GildedRoseKata
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = Host.CreateDefaultBuilder()
                .ConfigureServices(services => 
                {
                    services.AddTransient<RoseRunner>();
                }).Build();

            host.Services.GetRequiredService<RoseRunner>().Run(args);
        }
    }
}
