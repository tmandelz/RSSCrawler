using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Webcrawler.Service.Core
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddOptions();
                    services.Configure<CrawlConfig>(hostContext.Configuration.GetSection("CrawlConfig"));
                    services.AddHostedService<Worker>();
                });
    }
}