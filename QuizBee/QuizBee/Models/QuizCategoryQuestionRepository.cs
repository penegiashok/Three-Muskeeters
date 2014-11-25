using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using QuizBee.Data;

namespace QuizBee.Models
{
    public class QuizCategoryQuestionRepository: IQuizCategoryQuestionRepository
    {
        QuizDataModel _data = new QuizDataModel();

        public QuizCategoryQuestionRepository()
        {
            
        }

        public IEnumerable<QuizQuestion> GetAllQuestionsbyCategory(int categoryid)
        {
            List<QuizQuestion> toreturn = new List<QuizQuestion>();
            QuizCategory cat = _data.GetCategory(categoryid);
            if(cat!=null)
            {
                List<Question> result= _data.GetAllQuestionsforCategoryID(categoryid);
                foreach (Question qn in result)
                {
                    QuestionAnswer correctans= qn.QuestionAnswers.FirstOrDefault();
                    int coutofoptions=qn.QuestionOptions.Count();
                    if(coutofoptions >2)
                    {
                        QuestionOption option1=qn.QuestionOptions.FirstOrDefault();
                        QuestionOption option2=qn.QuestionOptions.Skip(1).First();
                        QuestionOption option3=qn.QuestionOptions.Skip(2).First();
                        QuestionOption option4=qn.QuestionOptions.Skip(3).First();
                        if(correctans!=null)
                        {
                            toreturn.Add(new QuizQuestion { QuestionID = qn.QuestionID, Question = qn.QuestionDesc, CorrectAnswer = new QuizAnswer { AnswerID = correctans.AnswerID, Answer = correctans.Answer.AnswerDesc }, Option1 = new QuizAnswer { AnswerID = option1.Answer.AnswerID, Answer = option1.Answer.AnswerDesc }, Option2 = new QuizAnswer { AnswerID = option2.Answer.AnswerID, Answer = option2.Answer.AnswerDesc }, Option3 = new QuizAnswer { AnswerID = option3.Answer.AnswerID, Answer = option3.Answer.AnswerDesc }, Option4 = new QuizAnswer { AnswerID = option4.Answer.AnswerID, Answer = option4.Answer.AnswerDesc }, Questioncategory = new QuizzCategory { CategoryID=cat.CategoryID, CategoryName=cat.Name, CategoryDescription=cat.Description} });
                        }
                    }
                    else
                    {
                        QuestionOption option1 = qn.QuestionOptions.FirstOrDefault();
                        QuestionOption option2 = qn.QuestionOptions.Skip(1).First();
                        if (correctans != null)
                        {
                            toreturn.Add(new QuizQuestion { QuestionID = qn.QuestionID, Question = qn.QuestionDesc, CorrectAnswer = new QuizAnswer { AnswerID = correctans.AnswerID, Answer = correctans.Answer.AnswerDesc }, Option1 = new QuizAnswer { AnswerID = option1.Answer.AnswerID, Answer = option1.Answer.AnswerDesc }, Option2 = new QuizAnswer { AnswerID = option2.Answer.AnswerID, Answer = option2.Answer.AnswerDesc }, Option3 = null, Option4 = null, Questioncategory = new QuizzCategory { CategoryID = cat.CategoryID, CategoryName = cat.Name, CategoryDescription = cat.Description } });
                        }
                    }
                }
            }
            return toreturn;
        }

        public QuizQuestion Get(int id)
        {
            QuizQuestion toreturn = new QuizQuestion();
            Question result = _data.GetQuestion(id);
            QuizCategory cat= _data.GetCategoryforaQuestion(id);
            if (result != null&& cat!=null)
            {

                QuestionAnswer correctans = result.QuestionAnswers.FirstOrDefault();
                int coutofoptions = result.QuestionOptions.Count();
                if (coutofoptions > 2)
                {
                    QuestionOption option1 = result.QuestionOptions.FirstOrDefault();
                    QuestionOption option2 = result.QuestionOptions.Skip(1).First();
                    QuestionOption option3 = result.QuestionOptions.Skip(2).First();
                    QuestionOption option4 = result.QuestionOptions.Skip(3).First();
                    if (correctans != null)
                    {
                        toreturn = new QuizQuestion { QuestionID = result.QuestionID, Question = result.QuestionDesc, CorrectAnswer = new QuizAnswer { AnswerID = correctans.AnswerID, Answer = correctans.Answer.AnswerDesc }, Option1 = new QuizAnswer { AnswerID = option1.Answer.AnswerID, Answer = option1.Answer.AnswerDesc }, Option2 = new QuizAnswer { AnswerID = option2.Answer.AnswerID, Answer = option2.Answer.AnswerDesc }, Option3 = new QuizAnswer { AnswerID = option3.Answer.AnswerID, Answer = option3.Answer.AnswerDesc }, Option4 = new QuizAnswer { AnswerID = option4.Answer.AnswerID, Answer = option4.Answer.AnswerDesc }, Questioncategory = new QuizzCategory { CategoryID = cat.CategoryID, CategoryName = cat.Name, CategoryDescription = cat.Description } };
                    }
                }
                else
                {
                    QuestionOption option1 = result.QuestionOptions.FirstOrDefault();
                    QuestionOption option2 = result.QuestionOptions.Skip(1).First();
                    if (correctans != null)
                    {
                        toreturn = new QuizQuestion { QuestionID = result.QuestionID, Question = result.QuestionDesc, CorrectAnswer = new QuizAnswer { AnswerID = correctans.AnswerID, Answer = correctans.Answer.AnswerDesc }, Option1 = new QuizAnswer { AnswerID = option1.Answer.AnswerID, Answer = option1.Answer.AnswerDesc }, Option2 = new QuizAnswer { AnswerID = option2.Answer.AnswerID, Answer = option2.Answer.AnswerDesc }, Option3 = null, Option4 = null, Questioncategory = new QuizzCategory { CategoryID = cat.CategoryID, CategoryName = cat.Name, CategoryDescription = cat.Description } };
                    }
                }
            }
            return toreturn;
        }

        public QuizQuestion Add(QuizQuestion question)
        {
            QuizQuestion result = new QuizQuestion();
            if (question == null)
            {
                throw new ArgumentNullException("Question");
            }

            try
            {
                using (QuizEntities Qz = new QuizEntities())
                {
                    Question toreturn = new Question();
                    using (var dbContextTransaction = Qz.Database.BeginTransaction())
                    {
                        try
                        {
                            Question newquestion = new Question();

                            newquestion.QuestionDesc = question.Question;

                            Qz.Questions.Add(newquestion);

                            Qz.SaveChanges();

                            int newIQstid=(from newqt in Qz.Questions
                                               where newqt.QuestionDesc==question.Question
                                               select newqt.QuestionID).FirstOrDefault();
                            if(newIQstid!=null&&newIQstid!=0)
                            {
                                if(question.Option1!=null&& question.Option1.AnswerID!=null)
                                {
                                    QuestionOption Qopt1=new QuestionOption();
                                    Qopt1.QuestionID=newIQstid;
                                    Qopt1.choiceID=question.Option1.AnswerID;
                                    Qz.QuestionOptions.Add(Qopt1);
                                    
                                }
                                if(question.Option2!=null&& question.Option2.AnswerID!=null)
                                {
                                    QuestionOption Qopt2=new QuestionOption();
                                    Qopt2.QuestionID=newIQstid;
                                    Qopt2.choiceID=question.Option2.AnswerID;
                                    Qz.QuestionOptions.Add(Qopt2);
                                    
                                }
                                if(question.Option3!=null&& question.Option3.AnswerID!=null)
                                {
                                    QuestionOption Qopt3=new QuestionOption();
                                    Qopt3.QuestionID=newIQstid;
                                    Qopt3.choiceID=question.Option3.AnswerID;
                                    Qz.QuestionOptions.Add(Qopt3);
                                    
                                }
                                if(question.Option4!=null&& question.Option4.AnswerID!=null)
                                {
                                    QuestionOption Qopt4=new QuestionOption();
                                    Qopt4.QuestionID=newIQstid;
                                    Qopt4.choiceID=question.Option4.AnswerID;
                                    Qz.QuestionOptions.Add(Qopt4);
                                    
                                }

                                if(question.CorrectAnswer!=null&&question.CorrectAnswer.AnswerID!=null&&question.Questioncategory!=null&&question.Questioncategory.CategoryID!=null)
                                {
                                    QuestionAnswer correctans=new QuestionAnswer();
                                    correctans.AnswerID=question.CorrectAnswer.AnswerID;
                                    correctans.QuestionID=newIQstid;
                                    correctans.CategoryID=question.Questioncategory.CategoryID;
                                    Qz.QuestionAnswers.Add(correctans);
                                }
                                Qz.SaveChanges();
                            }

                            dbContextTransaction.Commit();
                            if (question.Option1 != null && question.Option2 != null && question.Option3 != null && question.Option4 != null)
                            {
                                result = new QuizQuestion { QuestionID = newIQstid, Question = question.Question, Questioncategory = new QuizzCategory { CategoryID = question.Questioncategory.CategoryID, CategoryName = question.Questioncategory.CategoryName, CategoryDescription = question.Questioncategory.CategoryDescription }, CorrectAnswer = new QuizAnswer { AnswerID = question.CorrectAnswer.AnswerID, Answer = question.CorrectAnswer.Answer }, Option1 = new QuizAnswer { AnswerID = question.Option1.AnswerID, Answer = question.Option1.Answer }, Option2 = new QuizAnswer { AnswerID = question.Option2.AnswerID, Answer = question.Option2.Answer }, Option3 = new QuizAnswer { AnswerID = question.Option3.AnswerID, Answer = question.Option3.Answer }, Option4 = new QuizAnswer { AnswerID = question.Option4.AnswerID, Answer = question.Option4.Answer } };
                            }
                            else
                            {
                                result = new QuizQuestion { QuestionID = newIQstid, Question = question.Question, Questioncategory = new QuizzCategory { CategoryID = question.Questioncategory.CategoryID, CategoryName = question.Questioncategory.CategoryName, CategoryDescription = question.Questioncategory.CategoryDescription }, CorrectAnswer = new QuizAnswer { AnswerID = question.CorrectAnswer.AnswerID, Answer = question.CorrectAnswer.Answer }, Option1 = new QuizAnswer { AnswerID = question.Option1.AnswerID, Answer = question.Option1.Answer }, Option2 = new QuizAnswer { AnswerID = question.Option2.AnswerID, Answer = question.Option2.Answer }};
                            }
                            return result;
                        }
                        catch (Exception ex)
                        {
                            dbContextTransaction.Rollback();
                            return null;
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void Remove(int id)
        {
            _data.DeleteQuestion(id);
        }

        public bool update(QuizQuestion question)
        {
            try
            {
                bool toreturn = false;
                using (QuizEntities Qz = new QuizEntities())
                {
                    using (var dbContextTransaction = Qz.Database.BeginTransaction())
                    {
                        try
                        {
                            if (question.QuestionID != null && question.QuestionID != 0)
                            {
                                var questionopts = (from qopt in Qz.QuestionOptions
                                                        where qopt.QuestionID == question.QuestionID && qopt.choiceID==question.Option1.AnswerID
                                                        select qopt).ToList();
                                int i=1;
                                if (questionopts. Count > 2)
                                {
                                    foreach (QuestionOption qopt in questionopts)
                                    {
                                        if (question.Option1 != null && question.Option2 != null && question.Option3 != null && question.Option4 != null)
                                        {
                                            if (i == 1)
                                            {
                                                qopt.choiceID = question.Option1.AnswerID;
                                                qopt.QuestionID = question.QuestionID;
                                            }
                                            else if (i == 2)
                                            {
                                                qopt.choiceID = question.Option2.AnswerID;
                                                qopt.QuestionID = question.QuestionID;
                                            }
                                            else if (i == 3)
                                            {
                                                qopt.choiceID = question.Option3.AnswerID;
                                                qopt.QuestionID = question.QuestionID;
                                            }
                                            else if (i == 4)
                                            {
                                                qopt.choiceID = question.Option4.AnswerID;
                                                qopt.QuestionID = question.QuestionID;
                                            }
                                            i++;
                                        }
                                        else
                                        {
                                            if (i == 1)
                                            {
                                                qopt.choiceID = question.Option1.AnswerID;
                                                qopt.QuestionID = question.QuestionID;
                                            }
                                            else if (i == 2)
                                            {
                                                qopt.choiceID = question.Option2.AnswerID;
                                                qopt.QuestionID = question.QuestionID;
                                            }
                                            else
                                            {
                                                Qz.QuestionOptions.Remove(qopt);
                                            }
                                            i++;
                                        }
                                    }
                                }
                                else
                                {
                                    if (question.Option1 != null && question.Option2 != null && question.Option3 != null && question.Option4 != null)
                                    {
                                        foreach (QuestionOption qopt in questionopts)
                                        {
                                            if (i == 1)
                                            {
                                                qopt.choiceID = question.Option1.AnswerID;
                                                qopt.QuestionID = question.QuestionID;
                                            }
                                            else
                                            {
                                                qopt.choiceID = question.Option2.AnswerID;
                                                qopt.QuestionID = question.QuestionID;
                                            }
                                            i++;
                                        }
                                        QuestionOption opt3 = new QuestionOption();
                                        opt3.choiceID = question.Option3.AnswerID;
                                        opt3.QuestionID = question.QuestionID;
                                        QuestionOption opt4 = new QuestionOption();
                                        opt4.choiceID = question.Option4.AnswerID;
                                        opt4.QuestionID = question.QuestionID;

                                        Qz.QuestionOptions.Add(opt3);
                                        Qz.QuestionOptions.Add(opt4);
                                    }
                                    else
                                    {
                                        foreach (QuestionOption qopt in questionopts)
                                        {
                                            if (i == 1)
                                            {
                                                qopt.choiceID = question.Option1.AnswerID;
                                                qopt.QuestionID = question.QuestionID;
                                            }
                                            else
                                            {
                                                qopt.choiceID = question.Option2.AnswerID;
                                                qopt.QuestionID = question.QuestionID;
                                            }
                                            i++;
                                        }
                                    }
                                }

                                var QA = (from qna in Qz.QuestionAnswers
                                          where qna.QuestionID==question.QuestionID
                                          select qna).FirstOrDefault();
                                if (QA != null)
                                {
                                    QA.AnswerID = question.CorrectAnswer.AnswerID;
                                    QA.CategoryID = question.Questioncategory.CategoryID;
                                }

                                Qz.SaveChanges();
                            }

                            dbContextTransaction.Commit();
                            toreturn = true;
                        }
                        catch (Exception ex)
                        {
                            dbContextTransaction.Rollback();
                            return toreturn ;
                        }

                    }
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