using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Webcrawler.DAL
{
    public class Category : BaseEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Id { get; set; }

        public Entry Entry { get; set; }
        public string Name { get; set; }

        public Category()
        {
            CreatedDate = DateTimeOffset.Now;
            UpdatedDate = DateTimeOffset.Now;
        }

        public Category(string categoryId)
        {
            Id = categoryId;
            UpdatedDate = DateTimeOffset.Now;
        }

        public Category categoryExists(string categoryName)
        {
            Category category = null;

            using (DatabaseContext context = new DatabaseContext())
            {
                category = context.Categories.Where(c => c.Name == categoryName).FirstOrDefault();
            }

            if (category != null)
            {
                return category;
            }
            else
            {
                return null;
            }
        }

        public Category createCategory(string categoryName)
        {
            using (DatabaseContext databaseContext = new DatabaseContext())
            {
                Category category = new Category()
                {
                    Name = categoryName,
                };

                //databaseContext.Categories.Add(category);
                //databaseContext.SaveChanges();

                //return databaseContext.Categories.OrderByDescending(p => p.UpdatedDate).FirstOrDefault();
                return category;
            }
        }
    }
}