using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuizBee.Models
{
    public class QuizQuestion
    {
        public int QuestionID { get; set; }
        public string Question { get; set; }

        public QuizAnswer CorrectAnswer { get; set; }
        public QuizAnswer Option1 { get; set; }
        public QuizAnswer Option2 { get; set; }
        public QuizAnswer Option3 { get; set; }
        public QuizAnswer Option4 { get; set; }

        public QuizCategory Questioncategory { get; set; }
    }
}