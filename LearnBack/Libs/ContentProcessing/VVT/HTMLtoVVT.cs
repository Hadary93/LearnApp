using HtmlAgilityPack;
using System.Globalization;
using System.Text.RegularExpressions;
using DataProcessing.Lib;

namespace ContentProcessing.VVT
{
    /// <summary>
    /// Process the transcript offered by the HTML.
    /// General rules : 
    /// sounds are included between stars *.....*
    /// text continuation is conducted through trailing and leading 3 dots. ex : Hallo... ...Zusammen
    /// 
    /// </summary>
    public class HTMLtoVVT
    {
        public static bool IsProcessingCompleted = false;
        public static List<Video> Videos { get; set; } = new();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data">IEnumerable consists of html doc and the name of the file following the naming convenction "<video-title>_<date>"</param>
        public static List<(string fileName,string content)> CreateVVT(object? obj)
        {
            if (obj == null) return new List<(string fileName, string content)>();

            var data = (IEnumerable<(HtmlDocument htmlDoc, string fileName)>)obj;

            if (data == null) return new List<(string fileName, string content)>();


            var htmlGroups = data.GroupBy(x => x.fileName.Split("_")[0]);

            var topicsContent = HTMLGroupsToSubtitle(htmlGroups).ToList();

            var topicsVTT = topicsContent.Select(x =>
            {
                return (x.Key, VideoToVVT(x.Value));
            }).ToList();



            var VVTs = topicsVTT.Select(x =>  ($"{x.Key}", x.Item2)).ToList();

            return VVTs;
        }
        public static string VideoToVVT(Video videoContent)
        {
            return "WEBVTT\n" + videoContent.Cues.Select(y => y.Id + "\n" + y.start + " --> " + y.end + "\n" + y.Text + "\n").Aggregate((a, b) => a + "\n" + b);
        }
        /// <summary>
        /// Get Cue of each HTML group (video)
        /// </summary>
        /// <param name="htmlGroups"></param>
        /// <returns></returns>
        public static Dictionary<string, Video> HTMLGroupsToSubtitle(IEnumerable<IGrouping<string, (HtmlDocument htmlDoc, string fileName)>> htmlGroups)
        {
            Dictionary<string, Video> content = new Dictionary<string, Video>();

            foreach (var htmlGroup in htmlGroups)
            {
                var orderedByDate = htmlGroup.OrderBy(x =>
                {
                    var dateString = x.fileName.Split("_")[1].Split(".")[0];

                    return DateTime.ParseExact(dateString, "yyyy-MM-ddTHH-mm-ss-fffZ", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal);
                });

                string previous = string.Empty;
                var id = 1;
                foreach (var html in orderedByDate)
                {
                    var beingEnd = html.htmlDoc.DocumentNode.SelectNodes("//p").Select(x => (x.GetAttributeValue("begin", ""), x.GetAttributeValue("end", ""))).First();
                    var text = html.htmlDoc.DocumentNode.SelectNodes("//span").Select(x => x.InnerText).Aggregate((a, b) => a + " " + b);
                    var start = DateTime.Parse(beingEnd.Item1).TimeOfDay.ToString(@"hh\:mm\:ss\.fff");
                    var end = DateTime.Parse(beingEnd.Item2).TimeOfDay.ToString(@"hh\:mm\:ss\.fff");
                    if (!content.ContainsKey(htmlGroup.Key)) content.Add(htmlGroup.Key, new Video());

                    if (!text.Equals(previous))
                    {
                        content[htmlGroup.Key].Cues.Add(new Cue() { start = start, end = end, Text = text, Id = id++ });
                    }

                    previous = text;
                }
            }

            return content;
        }
       
        /// <summary>
        /// Extract words from a text.
        /// </summary>
        /// <param name="content">text content</param>
        public static List<string> GetWords(string content)
        {
            List<string> wordsAndSymbols = SplitText(content);
            return wordsAndSymbols;
        }
        static List<string> SplitText(string text)
        {
            // Regex pattern to split words and keep punctuation with them
            string pattern = @"[\w'-]+|[^\w\s]+";

            // Find all matches (words or punctuation marks)
            MatchCollection matches = Regex.Matches(text, pattern);

            List<string> result = new List<string>();

            foreach (Match match in matches)
            {
                result.Add(match.Value);
            }

            return result;
        }
    }
}
