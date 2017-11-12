using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
