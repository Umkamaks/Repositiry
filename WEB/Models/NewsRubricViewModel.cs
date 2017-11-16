using System.Collections.Generic;
using BLL.DTO;

namespace WEB.Models
{
    public class NewsRubricViewModel
    {
        public int PageSize { get; set; }
        public NewsDTO NewsDTO { get; set; }
        public IEnumerable<NewsViewModel> NewsViewModel { get; set; }
        public IEnumerable<RubricViewModel> RubricViewModel { get; set; }
        public IEnumerable<SourceViewModel> SourceViewModel { get; set; }


    }
}