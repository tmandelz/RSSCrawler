using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Webcrawler.DAL
{
    public class Author
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Name { get; set; }

        public static bool authorExists(string authorName)
        {
            List<Author> authors = new List<Author>();
            using (var db = new DatabaseContext())
            {
                authors = db.Authors.Where(author => author.Name == authorName).ToList();
            }

            if (authors.Count() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static void createAuthor(string authorName)
        {
            Author author = new Author();
            author.Name = authorName;

            using (var db = new DatabaseContext())
            {
                db.Authors.Add(author);
                db.SaveChanges();
            }
        }
    }
}