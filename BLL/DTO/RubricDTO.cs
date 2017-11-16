using System.Collections.Generic;

namespace BLL.DTO
{
    public class RubricDTO
    {
        public int Id { get; set; }
        public string NameRubric { get; set; }
        public ICollection<NewsDTO> News { get; set; }
    }
}
