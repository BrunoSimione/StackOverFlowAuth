using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StackOverFlowAuth.Models
{
    public class Question
    {
        [Key]
        public int Id { get; set; }
        public string QuestionName { get; set; }
        [DisplayFormat(DataFormatString = "{0:G}")]
        public DateTime CreationDate { get; set; }
        public int ViewCount { get; set; }
        public int AnswerCount { get; set; }
        public int VoteCount { get; set; }
        public virtual List<Answer> Answers { get; set; }

        public virtual User User { get; set; }
        public virtual Category Category { get; set; }
        public virtual List<Vote> Votes { get; set; }
    }
}