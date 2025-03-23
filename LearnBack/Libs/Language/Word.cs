namespace LanguageLib
{
    public class Word
    {
        public int Id { get; set; }
        public string WordText { get; set; }
        public string? Translation { get; set; }
        public bool Favourite { get; set; }
        public int Difficulty { get; set; }
        public int WordCount { get; set; }
        public string? Group { get; set; }
    }
}
