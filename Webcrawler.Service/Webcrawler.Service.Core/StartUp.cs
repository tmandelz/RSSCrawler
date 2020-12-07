using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Webcrawler.Service.Core
{
    public class Startup
    {
        public IConfigurationRoot Configuration { get; set; }

        //public Startup(IHostingEnvironment env)
        //{
        //    var builder = new ConfigurationBuilder()
        //        .SetBasePath(env.ContentRootPath)
        //        .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

        //    Configuration = builder.Build();
        //}


        //public void ConfigureServices(IServiceCollection services)
        //{
        //    services.AddOptions();
        //    services.Configure<CrawlConfig>(hostContext.Configuration.GetSection("CrawlConfig"));
        //    services.AddHostedService<Worker>();
        //}
    }
}