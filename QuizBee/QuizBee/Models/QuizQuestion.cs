using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QuizBee.Models
{
    public class QuizQuestion
    {
        public int QuestionID { get; set; }
        [Required]
        public string Question { get; set; }

        [Required]
        public QuizAnswer CorrectAnswer { get; set; }
        public QuizAnswer Option1 { get; set; }
        public QuizAnswer Option2 { get; set; }
        public QuizAnswer Option3 { get; set; }
        public QuizAnswer Option4 { get; set; }

        public QuizzCategory Questioncategory { get; set; }
    }
}