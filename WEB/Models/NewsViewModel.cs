using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace WEB.Models
{
    public class NewsViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Название")]
        public string TitleNews { get; set; }
        [Display(Name = "Предосмотр")]
        public string PreviewNews { get; set; }
        [Display(Name = "Полная новость")]
        public string FullNews { get; set; }
        [Display(Name = "Дата создания")]
        public DateTime? CreateDateTimeNews { get; set; }
        [Display(Name = "Сылка на картинку")]
        public string StringImage { get; set; }
        public int? RubricId { get; set; }
        [Display(Name = "Рубрика")]
        public string RubricName { get; set; }
        public int? NewsSourceId { get; set; }
        [Display(Name = "Источник данных")]
        public string SourceName { get; set; }
    }
}