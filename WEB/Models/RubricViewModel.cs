using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BLL.DTO;

namespace WEB.Models
{
    public class RubricViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Название рубрики")]
        public string NameRubric { get; set; }

        public ICollection<NewsDTO> News{ get; set; }
    }
}