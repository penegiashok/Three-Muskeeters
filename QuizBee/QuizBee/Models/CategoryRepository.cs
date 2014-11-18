using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using QuizBee.Data;

namespace QuizBee.Models
{
    public class CategoryRepository: ICategoryRepository
    {
        QuizDataModel _data = new QuizDataModel();

        public IEnumerable<QuizzCategory> GetAll()
        {
            List<QuizzCategory> toreturn = new List<QuizzCategory>();
            List<QuizCategory> result = _data.GetAllCategories();
            foreach (QuizCategory cat in result)
            {
                toreturn.Add(new QuizzCategory { CategoryID = cat.CategoryID, CategoryName = cat.Name, CategoryDescription=cat.Description });
            }
            return toreturn;
        }

        public QuizzCategory Get(int id)
        {
            QuizzCategory toreturn = new QuizzCategory();
            QuizCategory result = _data.GetCategory(id);
            if (result != null)
            {
                toreturn = new QuizzCategory { CategoryID = result.CategoryID, CategoryName = result.Name, CategoryDescription=result.Description };
            }
            return toreturn;
        }

        public QuizzCategory Add(QuizzCategory cat)
        {
            QuizCategory result = new QuizCategory();
            if (cat == null)
            {
                throw new ArgumentNullException("cat");
            }

            if (_data.IsCategoryExists(cat.CategoryName))
            {
                throw new ArgumentException("category already exists");
            }
            else
            {
                _data.AddCategory(cat.CategoryName,cat.CategoryDescription);
                result = _data.GetCategory(cat.CategoryName);
            }
            if (result != null)
            {
                return new QuizzCategory { CategoryID = result.CategoryID, CategoryName = result.Name, CategoryDescription=result.Description };
            }
            else
            {
                return null;
            }

        }

        public void Remove(int id)
        {
            _data.DeleteCategory(id);
        }

        public bool update(QuizzCategory cat)
        {
            return _data.UpdateCategory(new QuizCategory { CategoryID = cat.CategoryID, Name=cat.CategoryName, Description=cat.CategoryDescription});
        }
    }
}