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

        public static async IAsyncEnumerable<(string byOne, string whole)> GetIncrementalStatsAsync(int amountOfWords, string[] files)
        {
            var fileWordCounts = new Dictionary<string, List<KeyValuePair<string, int>>>();
            var totalWordCount = new Dictionary<string, int>();
            var sbByOne = new StringBuilder();
            var sbWhole = new StringBuilder();

            var stopwatch = System.Diagnostics.Stopwatch.StartNew();

            foreach (var file in files)
            {
                var lines = await File.ReadAllLinesAsync(file);

                var wordCount = new Dictionary<string, int>();
                foreach (var line in lines)
                {
                    var word = line.Trim();
                    if (string.IsNullOrEmpty(word))
                        continue;

                    // per file
                    if (wordCount.ContainsKey(word))
                        wordCount[word]++;
                    else
                        wordCount[word] = 1;

                    // whole
                    if (totalWordCount.ContainsKey(word))
                        totalWordCount[word]++;
                    else
                        totalWordCount[word] = 1;
                }

                var topFileWords = wordCount
                    .OrderByDescending(kvp => kvp.Value)
                    .Take(amountOfWords)
                    .ToList();
                fileWordCounts[System.IO.Path.GetFileName(file)] = topFileWords;

                sbByOne.AppendLine($"File: {System.IO.Path.GetFileName(file)}");
                foreach (var word in topFileWords)
                {
                    sbByOne.AppendLine($"{word.Key}: {word.Value}");
                }
                sbByOne.AppendLine();

                var topWholeWords = totalWordCount
                    .OrderByDescending(kvp => kvp.Value)
                    .Take(amountOfWords)
                    .ToList();

                sbWhole.Clear();
                sbWhole.AppendLine("Top 10 words in all files together:");
                foreach (var word in topWholeWords)
                {
                    sbWhole.AppendLine($"{word.Key}: {word.Value}");
                }

                yield return (
                    $"Time: {stopwatch.ElapsedMilliseconds} ms\n\n{sbByOne}",
                    $"Time: {stopwatch.ElapsedMilliseconds} ms\n\n{sbWhole}"
                );
            }
        }
    }
}
