using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StackOverFlowAuth.Models
{
    public class Answer
    {
        [Key]
        public int Id { get; set; }
        [DataType(DataType.MultilineText)]
        public string Text { get; set; }
        public DateTime CreationDate { get; set; }

        public virtual User User { get; set; }
        public virtual Question Question { get; set; }
    }
}