using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BLL.DTO;

namespace WEB.Models
{
    public class RubricViewModel
    {
        public int Id { get; set; }
        public string NameRubric { get; set; }
        public IEnumerable News{ get; set; }
    }
}