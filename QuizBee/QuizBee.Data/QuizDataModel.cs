using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizBee.Data
{
    public class QuizDataModel
    {
        public List<string> getlistofcategories()
        {
            try
            {
                List<string> toreturn = new List<string>();

                using (QuizEntities qz = new QuizEntities())
                {
                    toreturn = (from ai in qz.QuizCategories select ai.Name).ToList();
                }
                return toreturn;
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        public List<Question> GettenrandomQuestionsforaGivenCategory(string categoryname)
        {
            List<Question> toreturn = new List<Question>();
            try
            {
                using (QuizEntities qz = new QuizEntities())
                {
                    var questions = (from question in qz.Questions 
                                join questionanswer in qz.QuestionAnswers 
                                on  question.QuestionID equals questionanswer.QuestionID
                                join cat in qz.QuizCategories on questionanswer.CategoryID equals cat.CategoryID
                                where cat.Name == categoryname
                                select question);

                    toreturn = questions.OrderBy(x => Guid.NewGuid()).Take(10).ToList();
                }
                return toreturn;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public QuizSession GetTenRandomQuestionsforaGivenCategory(string categoryname)
        {
            QuizSession toreturn = new QuizSession();
            try
            {
                using (QuizEntities qz = new QuizEntities())
                {
                    var questions = (from question in qz.Questions
                                     join questionanswer in qz.QuestionAnswers
                                     on question.QuestionID equals questionanswer.QuestionID
                                     join cat in qz.QuizCategories on questionanswer.CategoryID equals cat.CategoryID
                                     where cat.Name == categoryname
                                     select question);

                    var  Tenquestions = questions.OrderBy(x => Guid.NewGuid()).Take(10).ToList();

                    var questioncategory = (from question in qz.Questions
                                            join questionanswer in qz.QuestionAnswers
                                            on question.QuestionID equals questionanswer.QuestionID
                                            join cat in qz.QuizCategories on questionanswer.CategoryID equals cat.CategoryID
                                            where cat.Name == categoryname
                                            select cat).FirstOrDefault();

                    toreturn.CategoryID = questioncategory.CategoryID;
                    toreturn.CategoryName = questioncategory.Name;
                    toreturn.CategoryDesc = questioncategory.Description;
                    toreturn.Questionlist = new List<CategoryQuestion>();

                    foreach(Question qn in Tenquestions)
                    {
                        CategoryQuestion catquestion = new CategoryQuestion();
                        catquestion.QuestionID = qn.QuestionID;
                        catquestion.QuestionDescription = qn.QuestionDesc;
                        
                        var qnOptns = (from options in qz.QuestionOptions
                                      join ans in qz.Answers on options.choiceID equals ans.AnswerID
                                      where options.QuestionID == qn.QuestionID
                                      select new { qnid = options.QuestionID, choiceid = options.OptionID, choicedesc = ans.AnswerDesc }).ToList();

                        foreach (var optn in qnOptns)
                        {
                            if(string.IsNullOrEmpty(catquestion.Option1))
                            {
                                catquestion.Option1ID = optn.choiceid;
                                catquestion.Option1 = optn.choicedesc;
                            }
                            else if (string.IsNullOrEmpty(catquestion.Option2))
                            {
                                catquestion.Option2ID = optn.choiceid;
                                catquestion.Option2 = optn.choicedesc;
                            }
                            else if (string.IsNullOrEmpty(catquestion.Option3))
                            {
                                catquestion.Option3ID = optn.choiceid;
                                catquestion.Option3 = optn.choicedesc;
                            }
                            else if (string.IsNullOrEmpty(catquestion.Option4))
                            {
                                catquestion.Option4ID = optn.choiceid;
                                catquestion.Option4 = optn.choicedesc;
                            }
                        }

                        var questionans = (from qsn in qz.Questions
                                       join Qnans in qz.QuestionAnswers on qsn.QuestionID equals Qnans.QuestionID
                                       join ans in qz.Answers on Qnans.AnswerID equals ans.AnswerID
                                       where qsn.QuestionID == qn.QuestionID
                                       select new { ansid=ans.AnswerID, ansdesc=ans.AnswerDesc }).FirstOrDefault();

                        catquestion.CorrectAnswerID = questionans.ansid;
                        catquestion.CorrectAnswer = questionans.ansdesc;

                        if (!string.IsNullOrEmpty(catquestion.Option1) && !string.IsNullOrEmpty(catquestion.Option2) && !string.IsNullOrEmpty(catquestion.Option3) && !string.IsNullOrEmpty(catquestion.Option4))
                        {
                            catquestion.HasFourOptions = true;
                        }
                        else
                        {
                            catquestion.HasFourOptions = false;
                        }

                        toreturn.Questionlist.Add(catquestion);
                    }


                }
                return toreturn;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public int AddAnswer(string answer)
        {
            try
            {
                int toreturn = 0;
                using (QuizEntities qz = new QuizEntities())
                {
                    Answer newanswer = new Answer();

                    newanswer.AnswerDesc = answer;

                    qz.Answers.Add(newanswer);
                    toreturn= qz.SaveChanges();
                }
                return toreturn;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public bool IsAnswerExists(string answer)
        {
            bool toreturn = false;
            try
            {
                using (QuizEntities qz = new QuizEntities())
                {
                    var result = (from ans in qz.Answers
                                  where ans.AnswerDesc == answer
                                  select ans).FirstOrDefault();
                    if (result != null && !string.IsNullOrEmpty(result.AnswerID.ToString()))
                    {
                        toreturn = true;
                    }
                }
                return toreturn;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public int DeleteAnswer(string answer)
        {
            try
            {
                int toreturn = 0;
                using (QuizEntities qz = new QuizEntities())
                {
                    var result = (from ans in qz.Answers
                                  where ans.AnswerDesc == answer
                                  select ans).FirstOrDefault();
                    if (result != null)
                    {
                        qz.Answers.Remove(result);
                    }

                    toreturn= qz.SaveChanges();
                }
                return toreturn;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void AddQuestion(int categoryid, string question, int option1id, int option2id, int option3id, int option4id, int answerid)
        {
            try
            {
                using (QuizEntities Qz = new QuizEntities())
                {
                    using (var dbContextTransaction = Qz.Database.BeginTransaction()) 
                    {
                        try
                        {
                            Question newquestion = new Question();

                            newquestion.QuestionDesc = question;

                            Qz.Questions.Add(newquestion);

                            Qz.SaveChanges();

                            int questionid = (from Qtn in Qz.Questions
                                             where Qtn.QuestionDesc == question
                                             select Qtn.QuestionID).FirstOrDefault();
                            if (questionid != null && questionid != 0)
                            {
                                if (option1id != 0 && option1id != null)
                                {
                                    QuestionOption Qopt1 = new QuestionOption();
                                    Qopt1.QuestionID = questionid;
                                    Qopt1.choiceID = option1id;
                                    Qz.QuestionOptions.Add(Qopt1);
                                }
                                if (option2id != 0 && option2id != null)
                                {
                                    QuestionOption Qopt2 = new QuestionOption();
                                    Qopt2.QuestionID = questionid;
                                    Qopt2.choiceID = option2id;
                                    Qz.QuestionOptions.Add(Qopt2);
                                }
                                if (option3id != 0 && option3id != null)
                                {
                                    QuestionOption Qopt3 = new QuestionOption();
                                    Qopt3.QuestionID = questionid;
                                    Qopt3.choiceID = option3id;
                                    Qz.QuestionOptions.Add(Qopt3);
                                }
                                if (option4id != 0 && option4id != null)
                                {
                                    QuestionOption Qopt4 = new QuestionOption();
                                    Qopt4.QuestionID = questionid;
                                    Qopt4.choiceID = option4id;
                                    Qz.QuestionOptions.Add(Qopt4);
                                }

                                QuestionAnswer newQna = new QuestionAnswer();

                                newQna.AnswerID = answerid;
                                newQna.QuestionID = questionid;
                                newQna.CategoryID = categoryid;

                                Qz.QuestionAnswers.Add(newQna);

                                Qz.SaveChanges();
                            }
                            else
                            {
                                dbContextTransaction.Rollback();
                                return;
                            }

                            dbContextTransaction.Commit();
                        }
                        catch (Exception ex)
                        {
                            dbContextTransaction.Rollback();
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void DeleteQuestion(int questionid)
        {
            try
            {
                using (QuizEntities Qz = new QuizEntities())
                {
                    using (var dbContextTransaction = Qz.Database.BeginTransaction())
                    {
                        try
                        {
                            // remove question from Questionanswer table 
                            var Qna=(from QNA in Qz.QuestionAnswers
                                    where QNA.QuestionID == questionid
                                    select QNA).FirstOrDefault();

                            if(Qna !=null)
                            {
                                Qz.QuestionAnswers.Remove(Qna);
                                Qz.SaveChanges();
                            }

                            var Qopts =(from options in Qz.QuestionOptions
                                            where options.QuestionID==questionid
                                            select options);

                            foreach(QuestionOption qop in Qopts)
                            {
                                Qz.QuestionOptions.Remove(qop);
                            }

                            Qz.SaveChanges();

                            var Qtn =(from Qn in Qz.Questions
                                          where Qn.QuestionID == questionid
                                          select Qn).FirstOrDefault();
                            if(Qtn !=null)
                            {
                                Qz.Questions.Remove(Qtn);
                            }

                            Qz.SaveChanges();
                            dbContextTransaction.Commit();
                        }
                        catch (Exception exp)
                        {
                            dbContextTransaction.Rollback();
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        public List<Question> GetAllQuestionsforaGivenCategory(string categoryname)
        {
            List<Question> toreturn = new List<Question>();
            try
            {
                using (QuizEntities qz = new QuizEntities())
                {
                    var questions = (from question in qz.Questions
                                     join questionanswer in qz.QuestionAnswers
                                     on question.QuestionID equals questionanswer.QuestionID
                                     join cat in qz.QuizCategories on questionanswer.CategoryID equals cat.CategoryID
                                     where cat.Name == categoryname
                                     select question).ToList();

                    toreturn = questions;
                }
                return toreturn;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<Question> GetAllQuestionsforCategoryID(int categoryid)
        {
            List<Question> toreturn = new List<Question>();
            try
            {
                using (QuizEntities qz = new QuizEntities())
                {
                    var questions = (from question in qz.Questions
                                     join questionanswer in qz.QuestionAnswers
                                     on question.QuestionID equals questionanswer.QuestionID
                                     join cat in qz.QuizCategories on questionanswer.CategoryID equals cat.CategoryID
                                     where cat.CategoryID ==categoryid
                                     select question).ToList();

                    toreturn = questions;
                }
                return toreturn;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Dictionary<int,string> getlistofcategorieswithids()
        {
            try
            {
                Dictionary<int, string> toreturn = new Dictionary<int, string>();

                using (QuizEntities Qz = new QuizEntities())
                {
                    var cats = (from catgs in Qz.QuizCategories
                                select catgs);

                    foreach (QuizCategory ctgory in cats)
                    {
                        toreturn.Add(ctgory.CategoryID, ctgory.Name);
                    }
                }
                return toreturn;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public int AddCategory(string categoryname, string categorydesc)
        {
            try
            {
                int result = 0;
                using (QuizEntities Qz = new QuizEntities())
                {
                    QuizCategory newcat = new QuizCategory();

                    newcat.Name = categoryname;
                    newcat.Description = categorydesc;

                    Qz.QuizCategories.Add(newcat);

                    result= Qz.SaveChanges();
                }
                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public int DeleteCategory(int categoryid)
        {
            try
            {
                int toreturn = 0;
                using (QuizEntities Qz = new QuizEntities())
                {
                    var deleteCat = (from cat in Qz.QuizCategories
                                     where cat.CategoryID == categoryid
                                     select cat).FirstOrDefault();
                    if (deleteCat != null)
                    {
                        Qz.QuizCategories.Remove(deleteCat);
                    }
                    toreturn=Qz.SaveChanges();
                }
                return toreturn;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
