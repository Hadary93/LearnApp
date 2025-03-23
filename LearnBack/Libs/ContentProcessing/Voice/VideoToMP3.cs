using ContentProcessing.Lib;
using DataProcessing.Lib;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContentProcessing.Voice
{
    public class VideoToMP3
    {
        private  string OutputPath { get; set; } = string.Empty;
        private  string InputFile { get; set; } = string.Empty;

        public VideoToMP3(string inputFile, string outputPath)
        {
            InputFile = inputFile;
            OutputPath = outputPath;
        }
        public void CreateRecords(List<Cue> cues)
        {
            foreach (var cue in cues)
            {
                var mp3FilePath = Path.Combine(OutputPath, $"{TextToHash.CreateHash(cue?.Text??string.Empty).ToString()}.mp3");
                var startTimeStamp = TimeSpan.Parse(cue?.start??string.Empty);
                var endTimeStamp = TimeSpan.Parse(cue?.end ?? string.Empty);

                CreateRecord(mp3FilePath, startTimeStamp.ToString(), (endTimeStamp - startTimeStamp).ToString());
            }
        }
        private void CreateRecord(string mp3FilePath,string startTime, string duration)
        {

            string arguments = $"-y -i \"{InputFile}\" -ss {startTime} -t {duration} -vn -acodec libmp3lame -q:a 2 \"{mp3FilePath}";

            Process process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "ffmpeg",
                    Arguments = arguments,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                }
            };

            process.Start();
            process.WaitForExit();
        }
    }
}
