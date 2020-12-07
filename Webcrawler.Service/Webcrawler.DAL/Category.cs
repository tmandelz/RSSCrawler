using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Webcrawler.DAL
{
    public class Category
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public string Name { get; set; }

        public static bool categoryExists(string categoryName)
        {
            List<Category> categories = new List<Category>();
            using (var db = new DatabaseContext())
            {
                categories = db.Categories.Where(category => category.Name == categoryName).ToList();
            }

            if (categories.Count() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static void createCategory(string categoryName)
        {
            Category category = new Category();
            category.Name = categoryName;

            using (var db = new DatabaseContext())
            {
                db.Categories.Add(category);

                db.SaveChanges();
            }
        }
    }
}