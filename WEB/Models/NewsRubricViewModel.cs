using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BLL.DTO;
using PagedList;

namespace WEB.Models
{
    public class NewsRubricViewModel
    {
        public NewsDTO NewsDTO { get; set; }
        public IEnumerable<NewsViewModel> NewsViewModel { get; set; }
        public IEnumerable<RubricViewModel> RubricViewModel { get; set; }
        public IEnumerable<SourceViewModel> SourceViewModel { get; set; }


    }
}