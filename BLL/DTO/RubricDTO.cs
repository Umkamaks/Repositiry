using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;

namespace BLL.DTO
{
    public class RubricDTO
    {
        public int Id { get; set; }
        public string NameRubric { get; set; }
        public ICollection<News> News { get; set; }
    }
}
