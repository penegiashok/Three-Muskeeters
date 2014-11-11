using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizBee.Data
{
    public class QuizSession
    {
        public int CategoryID { get; set; }

        public string CategoryName { get; set; }

        public string CategoryDesc { get; set; }

        public List<CategoryQuestion> Questionlist { get; set; }
    }
}
