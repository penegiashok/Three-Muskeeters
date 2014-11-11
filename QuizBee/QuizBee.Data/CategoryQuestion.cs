using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizBee.Data
{
    public class CategoryQuestion
    {
        public int QuestionID { get; set; }

        public string QuestionDescription { get; set; }

        public bool HasFourOptions { get; set; }

        public int Option1ID { get; set; }

        public string Option1 { get; set; }

        public int Option2ID { get; set; }

        public string Option2 { get; set; }

        public int Option3ID { get; set; }

        public string Option3 { get; set; }

        public int Option4ID { get; set; }

        public string Option4 { get; set; }

        public string CorrectAnswer { get; set; }

        public int CorrectAnswerID { get; set; }
    }
}
