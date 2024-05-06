
using GildedRoseKata;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

using VerifyXunit;

using Xunit;

namespace GildedRoseTests
{
    public class ApprovalTest
    {
        public IHost BuildTestHost() {
            return Host.CreateDefaultBuilder()
                .ConfigureServices(services => {
                    services.AddTransient<RoseRunner>();
                    services.AddSingleton<IWriter, TestWriter>();
                }).Build();
        }

        [Fact]
        public Task ThirtyDays()
        {
            var host = BuildTestHost();

            host.Services.GetRequiredService<RoseRunner>().Run(["30"]);

            var output = host.Services.GetRequiredService<IWriter>().ToString();

            return Verifier.Verify(output);
        }
    }

    public class TestWriter : IWriter
    {
        private StringBuilder textContents = new();

        public void WriteLine(string line)
        {
            textContents.AppendLine(line);
        }

        public override string ToString()
        {
            return textContents.ToString();
        }
    }
}
