using DataProcessing.Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ContentProcessing.VVT
{
    public class VVTtoCue
    {
        public static List<Cue> CreateCues(string vttContent)
        {
            // Define the regular expression pattern for parsing timestamps and text
            string pattern = @"(\d{2}:\d{2}:\d{2}\.\d{3} --> \d{2}:\d{2}:\d{2}\.\d{3})\s+([^\r\n]+)";

            // Create a list to store the extracted captions with start time
            List<Cue> captions = new List<Cue>();

            // Create a Regex object using the pattern
            Regex regex = new Regex(pattern);

            var index = 1;
            // Match the pattern in the VTT content
            foreach (Match match in regex.Matches(vttContent))
            {
                // Extract timestamp and text from the match
                string timestamp = match.Groups[1].Value;
                string text = match.Groups[2].Value;

                // Parse the start time (before the -->) from the timestamp
                string startTimestamp = timestamp.Split(" --> ")[0];
                string endTimestamp = timestamp.Split(" --> ")[1];
                TimeSpan startTime = TimeSpan.ParseExact(startTimestamp, @"hh\:mm\:ss\.fff", null);
                TimeSpan endTime = TimeSpan.ParseExact(endTimestamp, @"hh\:mm\:ss\.fff", null);

                // Add the result to the list
                captions.Add(new Cue() { Text = text, start = startTime.ToString(),  end = endTime.ToString(), Id= index++ });
            }

            return captions;
        }
    }
}
