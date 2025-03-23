using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageLib
{
    public class Paragraph
    {
        public int Id { get; set; }
        public ICollection<Sentence> Sentences { get; set; } = new List<Sentence>();    
    }
}
