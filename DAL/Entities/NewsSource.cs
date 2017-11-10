using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class NewsSource
    {
        public int Id { get; set; }

        public string NameSourceNews { get; set; }

        public ICollection<News> News { get; set; }

        public NewsSource()
        {
            News = new List<News>();
        }
    }
}
