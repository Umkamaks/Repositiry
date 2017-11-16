using System.Collections.Generic;

namespace DAL.Entities
{
    public class Rubric
    {
        public int Id { get; set; }

        public string NameRubric { get; set; }

        public ICollection<News> News { get; set; }

        public Rubric()
        {
            News = new List<News>();
        }

    }
}
