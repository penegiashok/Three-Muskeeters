using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using QuizBee.Data;

namespace QuizBee.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void GetCategories()
        {
            QuizDataModel dm = new QuizDataModel();
            List<string> result = new List<string>();
            result = dm.getlistofcategories(); ;
            Assert.IsTrue(result.Count>0);
        }

        [TestMethod]
        public void GetCategoryQuestions()
        {
            QuizDataModel dm = new QuizDataModel();
            List<Question> result = new List<Question>();
            result = dm.GettenrandomQuestionsforaGivenCategory("US State Capitals");
            Assert.IsTrue(result.Count>0);
        }

        [TestMethod]
        public void GetCategoryQuizSession()
        {
            QuizDataModel dm = new QuizDataModel();
            QuizSession result = new QuizSession();
            result = dm.GetTenRandomQuestionsforaGivenCategory("US State Capitals");
            Assert.IsTrue(result.Questionlist.Count > 0);
        }

        [TestMethod]
        public void Addanswer()
        {
            QuizDataModel dm = new QuizDataModel();
            int result = dm.AddAnswer("new test answer");
            Assert.AreEqual(result, 1);
        }

        [TestMethod]
        public void IsAnswerExists()
        {
            QuizDataModel dm = new QuizDataModel();
            bool result = dm.IsAnswerExists("new test1 answer");
            Assert.AreEqual(result, false);
        }

        [TestMethod]
        public void RemoveAnswer()
        {
            QuizDataModel dm = new QuizDataModel();
            int result = dm.DeleteAnswer("new test1 answer");
            Assert.AreEqual(result, 1);
        }

        [TestMethod]
        public void AddQuestion()
        {
            QuizDataModel dm = new QuizDataModel();
            dm.AddQuestion(1, "Minneapolis is captial city of Minnesota", 1, 2, 0, 0, 2);
        }

        [TestMethod]
        public void RemoveQuestion()
        {
            QuizDataModel dm = new QuizDataModel();
            dm.DeleteQuestion(51);
        }

        [TestMethod]
        public void GetAllQuestionsforCategoryID()
        {
            QuizDataModel dm = new QuizDataModel();
            List<Question> result= dm.GetAllQuestionsforCategoryID(1);
            Assert.IsTrue(result.Count > 0);
        }

        [TestMethod]
        public void GetListofcategoriesWithIds()
        {
            QuizDataModel dm = new QuizDataModel();
            Dictionary<int, string> result = dm.getlistofcategorieswithids();
            Assert.IsTrue(result.Count > 0);
        }
    }
}
