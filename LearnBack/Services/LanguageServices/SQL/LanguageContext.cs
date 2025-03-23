using LanguageLib;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace LanguageServices.SQL
{
    public class LanguageContext : DbContext
    {
        public DbSet<Word> Words { get; set; }

        public DbSet<Article> Articles { get; set; }
        public DbSet<Paragraph> Paragraphs { get; set; }
        public DbSet<Sentence> Sentences { get; set; }

        public LanguageContext(DbContextOptions<LanguageContext> options)
       : base(options)
        {
        }
    }
}
