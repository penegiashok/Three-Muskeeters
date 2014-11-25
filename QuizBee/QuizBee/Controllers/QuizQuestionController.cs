using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using QuizBee.Models;

namespace QuizBee.Controllers
{
    public class QuizQuestionController : ApiController
    {
        static readonly IQuizCategoryQuestionRepository repository = new QuizCategoryQuestionRepository();

        public IEnumerable<QuizQuestion> GetAllQuestionsbyCategory(int categoryid)
        {
            return repository.GetAllQuestionsbyCategory(categoryid);
        }

        public QuizQuestion GetQuestion(int id)
        {
            QuizQuestion result = repository.Get(id);
            if (result == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return result;
        }

        public HttpResponseMessage PostQuestion(QuizQuestion Question)
        {
            Question = repository.Add(Question);
            var response = Request.CreateResponse<QuizQuestion>(HttpStatusCode.Created, Question);

            string uri = Url.Link("QuizQuestion", new { id = Question.QuestionID });
            response.Headers.Location = new Uri(uri);
            return response;
        }

        public void PutQuestion(QuizQuestion question)
        {
            if (!repository.update(question))
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
        }

        public void DeleteQuestion(int id)
        {
            repository.Remove(id);
        }
    }
}
