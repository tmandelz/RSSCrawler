using System;
using System.Collections.Generic;
using System.Text;

namespace Webcrawler.DAL
{
    public class BaseEntity
    {
        public DateTimeOffset UpdatedDate { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
    }
}