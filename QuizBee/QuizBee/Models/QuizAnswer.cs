using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QuizBee.Models
{
    public class QuizAnswer
    {
        public int AnswerID { get; set; }
        [Required]
        public string Answer { get; set; }
    }
}