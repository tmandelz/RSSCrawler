using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Webcrawler.DAL
{
    public class Content : BaseEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        public Entry Entry { get; set; }

        public string URL { get; set; }
        public DateTime SaveDate { get; set; }
        public string HTMLContent { get; set; }

        public Content()
        {
            UpdatedDate = DateTimeOffset.Now;
            CreatedDate = DateTimeOffset.Now;
        }

        public Content createContent(string Url, string HTMLContent)
        {
            using (DatabaseContext databaseContext = new DatabaseContext())
            {
                Content content = new Content()
                {
                    URL = Url,
                    HTMLContent = HTMLContent,
                    SaveDate = DateTime.Now,
                };

                //databaseContext.Contents.Add(content);
                //databaseContext.SaveChanges();

                //return databaseContext.Contents.OrderByDescending(p => p.UpdatedDate).FirstOrDefault();
                return content;
            }
        }
    }
}