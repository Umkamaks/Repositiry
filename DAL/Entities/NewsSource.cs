using System.Collections.Generic;

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
