using System.ComponentModel.DataAnnotations;

namespace WEB.Models
{
    public class SourceViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Название источника новости")]
        public string NameSourceNews { get; set; }
    }
}