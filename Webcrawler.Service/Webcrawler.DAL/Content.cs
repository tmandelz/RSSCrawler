using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Webcrawler.DAL
{
    public class Content
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string URL { get; set; }
        public DateTime SaveDate { get; set; }
        public string HTMLContent { get; set; }
    }
}