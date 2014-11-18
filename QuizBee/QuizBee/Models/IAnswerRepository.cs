using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizBee.Models
{
    public interface IAnswerRepository
    {
        IEnumerable<QuizAnswer> GetAll();
        QuizAnswer Get(int id);
        QuizAnswer Add(QuizAnswer ans);
        void Remove(int id);
        bool update(QuizAnswer ans);
    }
}
