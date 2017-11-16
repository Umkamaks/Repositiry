using System;
using DAL.Entities;

namespace BLL.DTO
{
    public class NewsDTO
    {
        public int Id { get; set; }    
        public string TitleNews { get; set; }
        public string PreviewNews { get; set; }
        public string FullNews { get; set; }
        public DateTime? CreateDateTimeNews { get; set; }
        public string StringImage { get; set; }
        public int? RubricId { get; set; }
        public Rubric Rubric { get; set; }
        public int? NewsSourceId { get; set; }
        public NewsSource NewsSource { get; set; }
    }
}
