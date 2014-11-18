using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuizBee.Models
{
    public interface ICategoryRepository
    {
        IEnumerable<QuizzCategory> GetAll();
        QuizzCategory Get(int id);
        QuizzCategory Add(QuizzCategory ans);
        void Remove(int id);
        bool update(QuizzCategory ans);
    }
}