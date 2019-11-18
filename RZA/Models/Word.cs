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

        public string content { set; get; }
        public string secret { set; get; }
    }
}
