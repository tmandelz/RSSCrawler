using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
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
        public CrawlConfig CrawlConfig;

        public Worker(ILogger<Worker> logger, IOptions<CrawlConfig> settings)
        {
            _logger = logger;
            CrawlConfig = (CrawlConfig)settings.Value;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (DatabaseContext db = new DatabaseContext())
                {
                    db.Database.Migrate();
                };

                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                webcrawler = new Webcrawler("");
                webcrawler.Crawl();
                await Task.Delay(10000, stoppingToken);
            }
        }
    }
}