using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Webcrawler.DAL
{
    public class Author : BaseEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        public Entry Entry { get; set; }
        public string Name { get; set; }

        public Author()
        {
            CreatedDate = DateTimeOffset.Now;
            UpdatedDate = DateTimeOffset.Now;
        }

        public Author(string authorId)
        {
            Id = authorId;
            UpdatedDate = DateTimeOffset.Now;
        }

        public Author authorExists(string authorName)
        {
            Author author = null;

            using (DatabaseContext context = new DatabaseContext())
            {
                author = context.Authors.Where(a => a.Name == authorName).FirstOrDefault();
            }

            if (author != null)
            {
                return author;
            }
            else
            {
                return null;
            }
        }

        public Author createAuthor(string authorName)
        {
            using (DatabaseContext databaseContext = new DatabaseContext())
            {
                Author author = new Author()
                {
                    Name = authorName,
                };

                //databaseContext.Authors.Add(author);
                //databaseContext.SaveChanges();

                //return databaseContext.Authors.OrderByDescending(p => p.UpdatedDate).FirstOrDefault();
                return author;
            }
        }
    }
}