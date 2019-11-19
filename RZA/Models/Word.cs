using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace RZA.Models
{
    public class Word
    {
        public int Id { set; get; }
        [Display(Name = "T E X T O")]
        public string content { set; get; }
        [Display(Name = "R S A")]
        public string secret { set; get; }
    }
}
