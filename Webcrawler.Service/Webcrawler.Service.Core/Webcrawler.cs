using Microsoft.Extensions.Options;
using System;
using System.ServiceModel.Syndication;
using System.Xml;
using Webcrawler.DAL;

namespace Webcrawler.Service.Core
{
    internal class Webcrawler
    {
        private string rssFeedField;

        public Webcrawler(string rssFeed)
        {
            rssFeedField = rssFeed;
        }

        public void Crawl()
        {
            try
            {
                // get http content from rss feed
                string url = rssFeedField;
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

                    if (!Entry.entryExists(item.Id))
                    {
                        //save to db
                        Entry.createEntry(item);
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }
    }
}