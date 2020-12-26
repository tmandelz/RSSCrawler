using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net;
using System.ServiceModel.Syndication;
using System.Xml;
using Webcrawler.DAL;

namespace Webcrawler.Service.Core
{
    internal class Webcrawler
    {
        private readonly RSSFeed RSSFeed;
        private readonly ILogger<Worker> _logger;
        public Webcrawler(RSSFeed RSSFeed, ILogger<Worker> logger)
        {
            _logger = logger;
            this.RSSFeed = RSSFeed;
        }

        public void Crawl()
        {
            _logger.LogInformation($"Start Crawling!");
            try
            {
                // get http content from rss feed
                string url = RSSFeed.FeedURL;
                XmlReader reader = XmlReader.Create(url);
                SyndicationFeed feed = SyndicationFeed.Load(reader);
                reader.Close();
                foreach (SyndicationItem item in feed.Items)
                {
                    //Check if provider exists
                    Provider provider = new Provider();
                    provider = provider.providerExists(RSSFeed.FeedProvider);
                    if (provider == null)
                    {
                        //Create Provider
                        provider = new Provider();
                        provider = provider.createProvider(RSSFeed.FeedProvider);
                    }
                    else
                    {
                        provider = null;
                    }

                    //Author
                    List<Author> authors = new List<Author>();
                    foreach (var authorRSS in item.Authors)
                    {
                        Author author = new Author();

                        if (author.authorExists(authorRSS.Name) == null)
                        {
                            //Create Author
                            author = new Author();
                            author = author.createAuthor(authorRSS.Name);
                        }
                        else
                        {
                            //TOOO: update Entry
                        }

                        authors.Add(author);
                    }

                    //Categories
                    List<Category> categories = new List<Category>();
                    foreach (var categoryRSS in item.Categories)
                    {
                        Category category = new Category();

                        if (category.categoryExists(categoryRSS.Name) == null)
                        {
                            //Create Author
                            category = new Category();
                            category = category.createCategory(categoryRSS.Name);
                        }
                        else
                        {
                            //TOOO: update Entry
                        }

                        categories.Add(category);
                    }

                    //Content
                    List<Content> contents = new List<Content>();
                    foreach (var linkRSS in item.Links)
                    {
                        Content content = new Content();
                        content = content.createContent(linkRSS.Uri.AbsoluteUri, GetHTMLContent(linkRSS.Uri.AbsoluteUri));
                        contents.Add(content);
                    }

                    // Entry handling
                    Entry entry = new Entry();

                    if (entry.entryExists(item.Id) == null)
                    {
                        //Create Entry
                        entry = new Entry();
                        entry.createEntry(item, provider, authors, categories, contents);
                    }
                    else
                    {
                        //TOOO: update Entry
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        private string GetHTMLContent(string Url)
        {
            using (WebClient webClient = new WebClient())
            {
                return webClient.DownloadString(Url);
            }
        }
    }
}