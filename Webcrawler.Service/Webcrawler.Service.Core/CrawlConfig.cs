using System.Collections.Generic;

namespace Webcrawler.Service.Core
{
    public class CrawlConfig
    {
        public CrawlConfig()
        {
        }
        public List<RSSFeed> RSS { get; set; }
    }

    public class RSSFeed
    {
        public string FeedProvider { get; set; }
        public string FeedURL { get; set; }
    }
}