using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace WpfApp
{
    public class FileProcesses
    {
        public static async Task<Dictionary<string, List<KeyValuePair<string, int>>>> GetTopUsedWordsAsync(int amountOfWords, string[] files)
        {
            var fileWordCounts = new Dictionary<string, List<KeyValuePair<string, int>>>();

            foreach (var file in files)
            {
                var wordCount = new Dictionary<string, int>();
                var lines = await File.ReadAllLinesAsync(file);

                foreach (var line in lines)
                {
                    var word = line.Trim();
                    if (string.IsNullOrEmpty(word))
                        continue;

                    if (wordCount.ContainsKey(word))
                        wordCount[word]++;
                    else
                        wordCount[word] = 1;
                }
                fileWordCounts[Path.GetFileName(file)] = wordCount
                    .OrderByDescending(fwc => fwc.Value)
                    .Take(amountOfWords)
                    .ToList();
            }

            return fileWordCounts;
        }
        public static Dictionary<string, int> GetTopWordsWholeAsyncBetter(string[] files)
        {
            var totalWordCount = new Dictionary<string, int>();

            foreach (var file in files)
            {
                var lines = File.ReadAllLines(file);

                foreach (var line in lines)
                {
                    var word = line.Trim();
                    if (string.IsNullOrEmpty(word))
                        continue;

                    if (totalWordCount.ContainsKey(word))
                        totalWordCount[word]++;
                    else
                        totalWordCount[word] = 1;
                }
            }

            return totalWordCount;
        }

        public static Dictionary<string, int> GetTopWordsWhole(string[] files)
        {
            var totalWordCount = new Dictionary<string, int>();

            foreach (var file in files)
            {
                var lines = File.ReadAllLines(file);

                foreach (var line in lines)
                {
                    var word = line.Trim();
                    if (string.IsNullOrEmpty(word))
                        continue;

                    if (totalWordCount.ContainsKey(word))
                        totalWordCount[word]++;
                    else
                        totalWordCount[word] = 1;
                }
            }

            return totalWordCount;
        }
    }
}
