using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using QuizBee.Models;

namespace QuizBee.Controllers
{
    public class QuizAnswerController : ApiController
    {
        static readonly IAnswerRepository repository = new AnswerRepository();

        public IEnumerable<QuizAnswer> GetAllAnswers()
        {
            return repository.GetAll();
        }

        public QuizAnswer GetAnswer(int id)
        {
            QuizAnswer result = repository.Get(id);
            if (result == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound); 
            }
            return result;
        }

        public HttpResponseMessage PostAnswer(QuizAnswer ans)
        {
            ans= repository.Add(ans);
            var response = Request.CreateResponse<QuizAnswer>(HttpStatusCode.Created, ans);

            string uri = Url.Link("QuizAnswer", new { id = ans.AnswerID });
            response.Headers.Location = new Uri(uri);
            return response;
        }

        public void PutAnswer(int id, QuizAnswer ans)
        {
            ans.AnswerID = id;
            if (!repository.update(ans))
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
        }

        public void DeleteAnswer(int id)
        {
            QuizAnswer ans = repository.Get(id);
            if (ans == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            repository.Remove(id);
        }
    }
}
