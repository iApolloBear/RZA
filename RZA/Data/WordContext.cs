using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RZA.Models;

namespace RZA.Data
{
    public class WordContext : DbContext
    {
        public WordContext(DbContextOptions<WordContext> options) : base(options)
        {

        }

        public DbSet<Word> Word { set; get; }
    }
}
