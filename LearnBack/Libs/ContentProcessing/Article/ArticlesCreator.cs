using ContentProcessing.Lib;
using ContentProcessing.Voice;
using ContentProcessing.VVT;
using DataProcessing.Lib;
using LanguageLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ContentProcessing.Article
{
    public class ArticlesCreator
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="articleName"></param>
        /// <param name="vvtContent"></param>
        /// <param name="timeThreshold">In seconds</param>
        /// <returns></returns>
        public static LanguageLib.Article CreateArticle(string articleName, string vvtContent, double timeThreshold=5)
        {
            LanguageLib.Article article = new LanguageLib.Article() {Name = articleName, Paragraphs = new List<Paragraph>() };

            List<(string timestamp, string text, TimeSpan startTime, TimeSpan endTime)> captions = ParseVtt(vvtContent);
            // Bundle captions based on timestamp difference
            List<List<(string timestamp, string text, TimeSpan startTime, TimeSpan endTime)>> paragraphs = BundleCaptions(captions, timeThreshold);

            foreach (var paragraph in paragraphs)
            {
                Paragraph para = new Paragraph() { Sentences = new List<Sentence>() };

                foreach (var sentence in paragraph)
                {

                    var sentenceHash = TextToHash.CreateHash(sentence.text).ToString();
                    //var avalaibleSentence = article.Paragraphs.SelectMany(x => x.Sentences).FirstOrDefault(x => x.Hash == sentenceHash);
                    //if (avalaibleSentence!=null)
                    //{
                    //    para.Sentences.Add(avalaibleSentence);
                    //    continue;
                    //}
                    var words = CreateWords(sentence.text);
                    List < Word> wordsObjects = new List<Word>();
                    foreach (var word in words)
                    {
                        //var avalaibleWord = article.Paragraphs.SelectMany(x => x.Sentences).SelectMany(x => x.Words).FirstOrDefault(x => words.Contains(x.WordText));
                        //if (avalaibleWord != null)
                        //{
                        //    wordsObjects.Add(avalaibleWord);
                        //}
                        //else {
                            wordsObjects.Add(new Word() { WordText = word });
                        //}
                    }       
                    para.Sentences.Add(new Sentence() { Hash = sentenceHash, Words = wordsObjects });
                }
                article.Paragraphs.Add(para);
            }
            return article;
        }
        public static List<(string timestamp, string text, TimeSpan startTime, TimeSpan endTime)> ParseVtt(string vttContent)
        {
            // Define the regular expression pattern for parsing timestamps and text
            string pattern = @"(\d{2}:\d{2}:\d{2}\.\d{3} --> \d{2}:\d{2}:\d{2}\.\d{3})\s+([^\r\n]+)";

            // Create a list to store the extracted captions with start time
            List<(string timestamp, string text, TimeSpan startTime, TimeSpan endTime)> captions = new List<(string, string, TimeSpan, TimeSpan)>();

            // Create a Regex object using the pattern
            Regex regex = new Regex(pattern);

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
                captions.Add((timestamp, text, startTime, endTime));
            }

            return captions;
        }
        public static List<List<(string timestamp, string text, TimeSpan startTime, TimeSpan endTime)>> BundleCaptions(List<(string timestamp, string text, TimeSpan startTime, TimeSpan endTime)> captions, double timeThreshold)
        {
            List<List<(string timestamp, string text, TimeSpan startTime, TimeSpan endTime)>> bundles = new List<List<(string timestamp, string text, TimeSpan startTime, TimeSpan endTime)>>();

            // Start the first bundle
            List<(string timestamp, string text, TimeSpan startTime, TimeSpan endTime)> currentBundle = new List<(string, string, TimeSpan startTime, TimeSpan endTime)>();
            currentBundle.Add(captions[0]);

            for (int i = 1; i < captions.Count; i++)
            {
                // Calculate the time difference between current caption and the previous one
                TimeSpan timeDifference = captions[i].startTime - captions[i - 1].endTime;

                // If the time difference is greater than the threshold, start a new bundle
                if (timeDifference.TotalSeconds > timeThreshold)
                {
                    // Add the current bundle to the list of bundles
                    bundles.Add(currentBundle);

                    // Start a new bundle
                    currentBundle = new List<(string, string, TimeSpan startTime, TimeSpan endTime)>();
                }

                // Add the current caption to the current bundle
                currentBundle.Add(captions[i]);
            }

            // Add the last bundle to the list
            if (currentBundle.Count > 0)
            {
                bundles.Add(currentBundle);
            }

            return bundles;
        }
        /// <summary>
        /// Extract words from a text.
        /// </summary>
        /// <param name="content">text content</param>
        public static List<string> CreateWords(string content)
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
