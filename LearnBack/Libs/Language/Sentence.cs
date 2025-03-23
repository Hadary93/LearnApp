using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageLib
{
    public class Sentence
    {
        public int Id { get; set; }
        public string Hash { get; set; }
        public ICollection<Word> Words { get; set; } = new List<Word>();
        public int Difficulty { get; set; }
        public int SentenceCount { get; set; }
    }
}
