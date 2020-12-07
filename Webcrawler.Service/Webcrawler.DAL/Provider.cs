using System.ComponentModel.DataAnnotations.Schema;

namespace Webcrawler.DAL
{
    public class Provider
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
