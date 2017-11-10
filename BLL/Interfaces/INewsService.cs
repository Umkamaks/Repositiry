using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.DTO;

namespace BLL.Interfaces
{
    public interface INewsService
    {
        NewsDTO GetNews(int? id);
        IEnumerable<NewsDTO> GetAllNews();

        NewsSourceDTO GetNewsSource(int? id);
        IEnumerable<NewsSourceDTO> GetAllNewsSources();

        RubricDTO GetRubric(int? id);
        IEnumerable<RubricDTO> GetAllRubrics();

        void Dispose();
    }
}
