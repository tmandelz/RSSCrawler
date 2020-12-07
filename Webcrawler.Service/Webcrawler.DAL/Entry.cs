using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.ServiceModel.Syndication;

namespace Webcrawler.DAL
{
    public class Entry
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        public string EntryId { get; set; }

        public string Summary { get; set; }

        public DateTimeOffset PublishDate { get; set; }
        public DateTimeOffset LastUpdatedTime { get; set; }




        public static bool entryExists(string entryId)
        {
            List<Entry> entries = new List<Entry>();
            using (var db = new DatabaseContext())
            {
                entries = db.Entries.Where(entry => entry.EntryId == entryId).ToList();
            }

            if (entries.Count() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static void createEntry(SyndicationItem item,Provider provider)
        {
            try
            {
                //Check if Author exists
                foreach (var author in item.Authors)
                {
                    if (!Author.authorExists(author.Name))
                    {
                        Author.createAuthor(author.Name);
                    }
                }

                //Check if Category exists
                foreach (var category in item.Categories)
                {
                    if (!Category.categoryExists(category.Name))
                    {
                        Category.createCategory(category.Name);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            try
            {
                using (var db = new DatabaseContext())
                {
                    Entry entry = new Entry
                    {
                        EntryId = item.Id,
                        PublishDate = item.PublishDate,
                        Summary = item.Title.Text,
                        LastUpdatedTime = item.LastUpdatedTime,
                    };
                    db.Entries.Add(entry);

                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
            }
        }
    }
}