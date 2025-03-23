using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageLib
{
    public class Article
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public ICollection<Paragraph> Paragraphs { get; set; } = new List<Paragraph>();
    }
}
