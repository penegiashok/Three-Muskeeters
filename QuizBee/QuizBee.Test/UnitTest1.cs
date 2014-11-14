using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using QuizBee.Data;
using QuizBee.Models;

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
        public void GetAllanswers()
        {
            QuizDataModel dm = new QuizDataModel();
            List<Answer> result= dm.GetAllanswers();
            Assert.IsTrue(result.Count > 0);
        }

        [TestMethod]
        public void Getanswer()
        {
            QuizDataModel dm = new QuizDataModel();
            Answer result = dm.Getanswer(5);
            Assert.IsTrue(result != null);
            Assert.IsTrue(!string.IsNullOrEmpty(result.AnswerID.ToString()));
            Assert.IsTrue(!string.IsNullOrEmpty(result.AnswerDesc));
        }

        public void Getanswerwithdesc()
        {
            QuizDataModel dm = new QuizDataModel();
            Answer result = dm.Getanswer("");
            Assert.IsTrue(result != null);
            Assert.IsTrue(!string.IsNullOrEmpty(result.AnswerID.ToString()));
            Assert.IsTrue(!string.IsNullOrEmpty(result.AnswerDesc));
        }

        [TestMethod]
        public void Addanswer()
        {
            QuizDataModel dm = new QuizDataModel();
            int result = dm.AddAnswer("new test answer");
            Assert.AreEqual(result, 1);
        }

        [TestMethod]
        public void UpdateAnswer()
        {
            QuizDataModel dm = new QuizDataModel();
            Answer updateans = new Answer();
            updateans.AnswerID=5;
            updateans.AnswerDesc="test";

            bool     result = dm.UpdateAnswer(updateans);
            Assert.AreEqual(result, true);
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
        public void RemovAnswerwithid()
        {
            QuizDataModel dm = new QuizDataModel();
            int result = dm.DeleteAnswer(4);
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

        // Model tests
        [TestMethod]
        public void GetAllModelAnswers()
        {
            AnswerRepository repository = new AnswerRepository();

            var result =repository.GetAll();
            Assert.IsTrue(result != null);
        }

        [TestMethod]
        public void GetModelAnswer()
        {
            AnswerRepository repository = new AnswerRepository();

            var result = repository.Get(5);
            Assert.IsTrue(result != null);
        }

        [TestMethod]
        public void AddModelAnswer()
        {
            AnswerRepository repository = new AnswerRepository();
            QuizAnswer addans = new QuizAnswer();
            addans.Answer="new test add ans";
            var result = repository.Add(addans);
            Assert.IsTrue(result != null);
        }

        [TestMethod]
        public void RemoveModelAnswer()
        {
            AnswerRepository repository = new AnswerRepository();
            repository.Remove(186);
        }

        [TestMethod]
        public void UpdateModelAnswer()
        {
            AnswerRepository repository = new AnswerRepository();
            QuizAnswer updateans = new QuizAnswer();
            updateans.Answer = "new test answer updated";
            updateans.AnswerID = 185;
            bool result=repository.update(updateans);
            Assert.IsTrue(result);
        }
    }
}
