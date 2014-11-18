using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using QuizBee.Models;

namespace QuizBee.Controllers
{
    public class QuizCategoryController : ApiController
    {
        static readonly ICategoryRepository repository = new CategoryRepository();

        public IEnumerable<QuizzCategory> GetAllcategories()
        {
            return repository.GetAll();
        }

        public QuizzCategory GetAnswer(int id)
        {
            QuizzCategory result = repository.Get(id);
            if (result == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return result;
        }

        public HttpResponseMessage PostAnswer(QuizzCategory cat)
        {
            cat = repository.Add(cat);
            var response = Request.CreateResponse<QuizzCategory>(HttpStatusCode.Created, cat);

            string uri = Url.Link("QuizzCategory", new { id = cat.CategoryID });
            response.Headers.Location = new Uri(uri);
            return response;
        }

        public void PutAnswer(int id, QuizzCategory cat)
        {
            cat.CategoryID = id;
            if (!repository.update(cat))
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
        }

        public void DeleteAnswer(int id)
        {
            QuizzCategory ans = repository.Get(id);
            if (ans == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            repository.Remove(id);
        }
    }
}
