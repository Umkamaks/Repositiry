using System.Collections.Generic;
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

        void SetNews(NewsDTO newsDTO);

        void UpdataNews(NewsDTO newsDTO);

        void DeleteNews(int? id);
        void DeleteRubric(int? id);

        void SetRubric(RubricDTO rubricDTO);
        void UpdataRubric(RubricDTO rubricDTO);

        void SetNewsSource(NewsSourceDTO newsSourceDTO);

        void UpdataSource(NewsSourceDTO newsSourceDTO);
        void DeleteSource(int? id);
        void Dispose();
    }
}
