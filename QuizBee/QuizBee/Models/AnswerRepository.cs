using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using QuizBee.Data;

namespace QuizBee.Models
{
    public class AnswerRepository: IAnswerRepository
    {
        QuizDataModel _data = new QuizDataModel();

        public AnswerRepository()
        {
            
        }

        public IEnumerable<QuizAnswer> GetAll()
        {
            List<QuizAnswer> toreturn = new List<QuizAnswer>();
            List<Answer> result= _data.GetAllanswers();
            foreach (Answer ans in result)
            {
                toreturn.Add(new QuizAnswer { AnswerID = ans.AnswerID, Answer = ans.AnswerDesc });
            }
            return toreturn;
        }

        public QuizAnswer Get(int id)
        {
            QuizAnswer toreturn = new QuizAnswer();
            Answer result=_data.Getanswer(id);
            if (result != null)
            {
                toreturn = new QuizAnswer { AnswerID = result.AnswerID, Answer = result.AnswerDesc };
            }
            return toreturn;
        }

        public QuizAnswer Add(QuizAnswer ans)
        {
            Answer result= new Answer();
            if (ans == null)
            {
                throw new ArgumentNullException("ans");
            }

            if (_data.IsAnswerExists(ans.Answer))
            {
                throw new ArgumentException("Answer already exists");
            }
            else
            {
                _data.AddAnswer(ans.Answer);
                result= _data.Getanswer(ans.Answer);
            }
            if (result != null)
            {
                return new QuizAnswer { AnswerID = result.AnswerID, Answer = result.AnswerDesc };
            }
            else
            {
                return null;
            }
            
        }

        public void Remove(int id)
        {
            _data.DeleteAnswer(id);
        }

        public bool update(QuizAnswer ans)
        {
            return _data.UpdateAnswer(new Answer { AnswerID = ans.AnswerID, AnswerDesc = ans.Answer });
        }
    }
}