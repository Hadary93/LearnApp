namespace DataProcessing.Lib
{
    public class Video
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<Cue> Cues { get; set; } = new();
    }
}
