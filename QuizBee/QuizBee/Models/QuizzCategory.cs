using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QuizBee.Models
{
    public class QuizzCategory
    {
        [Required]
        public string CategoryName { get; set; }
        public string CategoryDescription { get; set; }
        public int CategoryID { get; set; }
    }
}