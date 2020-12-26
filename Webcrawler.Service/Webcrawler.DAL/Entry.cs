using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net;
using System.ServiceModel.Syndication;

namespace Webcrawler.DAL
{
    public class Entry : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        public string EntryId { get; set; }

        public string Summary { get; set; }

        public DateTimeOffset PublishDate { get; set; }
        public DateTimeOffset LastUpdatedTime { get; set; }
        public ICollection<Content> Contents { get; set; }
        public ICollection<Category> Categories { get; set; }
        public ICollection<Author> Authors { get; set; }
        public Provider Provider { get; set; }

        public Entry()
        {
            UpdatedDate = DateTimeOffset.Now;
            CreatedDate = DateTimeOffset.Now;
        }

        public Entry(string entryId)
        {
            Id = entryId;
            UpdatedDate = DateTimeOffset.Now;
        }

        public Entry entryExists(string entryId)
        {
            Entry entry = null;
            using (var db = new DatabaseContext())
            {
                entry = db.Entries.Where(entry => entry.EntryId == entryId).FirstOrDefault();
            }

            if (entry != null)
            {
                return entry;
            }
            else
            {
                return null;
            }
        }

        public void createEntry(SyndicationItem item, Provider provider, List<Author> authors, List<Category> categories, List<Content> contents)
        {
            try
            {
                //TODO:primaryKey duplicate
                using (var db = new DatabaseContext())
                {
                    Entry entry = new Entry
                    {
                        EntryId = item.Id,
                        PublishDate = item.PublishDate,
                        Summary = item.Title.Text,
                        LastUpdatedTime = item.LastUpdatedTime,
                        Authors = authors,
                        Categories = categories,
                        Provider = provider,
                        Contents = contents
                    };
                    db.Entries.Add(entry);

                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                //TODO send Mail on error
            }
        }

        public void AddContent(string url)
        {
            string contentHTML;
            using (WebClient client = new WebClient())
            {
                contentHTML = client.DownloadString(url);
            }
            //TODO: add Content to this Entry
        }
    }
}