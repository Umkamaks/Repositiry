using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace WEB.Models
{
    public class NewsViewModel
    {
        public int Id { get; set; }
        public string TitleNews { get; set; }
        public string PreviewNews { get; set; }
        public string FullNews { get; set; }
        public DateTime? CreateDateTimeNews { get; set; }
        public string StringImage { get; set; }
        public int? RubricId { get; set; }
        public string RubricName { get; set; }
        public int? NewsSourceId { get; set; }
        public string SourceName { get; set; }
    }
}