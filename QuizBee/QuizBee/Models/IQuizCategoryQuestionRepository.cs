using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizBee.Models
{
    public interface IQuizCategoryQuestionRepository
    {
        IEnumerable<QuizQuestion> GetAllQuestionsbyCategory(int categoryid);
        QuizQuestion Get(int id);
        QuizQuestion Add(QuizQuestion ans);
        void Remove(int id);
        bool update(QuizQuestion ans);
    }
}
