using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace StackOverFlowAuth.Models
{
    public class Vote
    {
        [Key]
        public int Id { get; set; }
        [DisplayFormat(DataFormatString = "{0:G}")]
        public DateTime CreationDate { get; set; }
        public int Value { get; set; }
        public virtual User User { get; set; }
    }
}