using Microsoft.Extensions.Options;
using System;
using System.ServiceModel.Syndication;
using System.Xml;
using Webcrawler.DAL;

namespace Webcrawler.Service.Core
{
    internal class Webcrawler
    {
        private readonly RSSFeed RSSFeed;

        public Webcrawler(RSSFeed RSSFeed)
        {
            this.RSSFeed = RSSFeed;
        }

        public void Crawl()
        {
            try
            {
                // get http content from rss feed
                string url = RSSFeed.FeedURL;
                XmlReader reader = XmlReader.Create(url);
                SyndicationFeed feed = SyndicationFeed.Load(reader);
                reader.Close();
                foreach (SyndicationItem item in feed.Items)
                {
                    // Check if Entry exists

                    if (item.Authors.Count > 1)
                    {
                        Console.WriteLine($"More than one Author");
                    }

                    Provider provider = new Provider()
                    {
                        Name = RSSFeed.FeedProvider
                    };

                    if (!Entry.entryExists(item.Id))
                    {
                        //save to db
                        Entry.createEntry(item, provider);
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }
    }
}