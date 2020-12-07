using System.Collections.Generic;

namespace Webcrawler.Service.Core
{
    public class CrawlConfig
    {
        public CrawlConfig()
        {
        }

        public string ApplicationName { get; set; }

        public List<RSSFeed> rssFeeds = new List<RSSFeed>();
    }

    public class RSSFeed
    {
        public string FeedProvider { get; set; }
        public string FeedURL { get; set; }
    }
}