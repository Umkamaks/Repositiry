using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WEB.Models
{
    public class SourceViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Название источника новости")]
        public string NameSourceNews { get; set; }
    }
}