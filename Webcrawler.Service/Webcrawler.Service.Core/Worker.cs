using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using Webcrawler.DAL;

namespace Webcrawler.Service.Core
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private Webcrawler webcrawler;
        private readonly CrawlConfig CrawlConfig;

        public Worker(ILogger<Worker> logger, CrawlConfig settings)
        {
            _logger = logger;
            this.CrawlConfig = settings;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (DatabaseContext db = new DatabaseContext())
                {
                    db.Database.EnsureCreated();
                };

                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

                foreach (var item in CrawlConfig.RSS)
                {
                    webcrawler = new Webcrawler(item, _logger);
                    webcrawler.Crawl();
                }
                await Task.Delay(10000, stoppingToken);
            }
        }
    }
}