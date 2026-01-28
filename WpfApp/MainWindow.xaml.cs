using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnByOne_Click(object sender, RoutedEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Wait;
            Stopwatch time = Stopwatch.StartNew();
            const int amountOfWords = 10;
            const string filepath = "D:\\repos\\CNET2\\bigFiles";
            var files = Directory.GetFiles(filepath, "*.*");
            var fileWordCounts = new Dictionary<string, List<KeyValuePair<string, int>>>();

            foreach (var file in files)
            {
                var wordCount = new Dictionary<string, int>();
                var lines = File.ReadAllLines(file);

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
                fileWordCounts[System.IO.Path.GetFileName(file)] = wordCount
                    .OrderByDescending(fwc => fwc.Value)
                    .Take(amountOfWords)
                    .ToList();
            }

            var sb = new StringBuilder();
            foreach (var fwc in fileWordCounts)
            {
                sb.AppendLine($"File: {fwc.Key}");
                foreach (var word in fwc.Value)
                {
                    sb.AppendLine($"{word.Key}: {word.Value}");
                }
                sb.AppendLine();
            }
            time.Stop();
            txtBox1.Text = "Time: " + time.ElapsedMilliseconds.ToString() + " ms\n\n";
            txtBox1.Text += sb.ToString();
            Mouse.OverrideCursor = null;
        }

        private void btnWhole_Click(object sender, RoutedEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Wait;
            Stopwatch time = Stopwatch.StartNew();
            const int amountOfWords = 10;
            const string filepath = "D:\\repos\\CNET2\\bigFiles";
            var files = Directory.GetFiles(filepath, "*.*");

            // Dictionary to count words across all files
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

            var topWords = totalWordCount
                .OrderByDescending(kvp => kvp.Value)
                .Take(amountOfWords)
                .ToList();

            var sb = new StringBuilder();
            sb.AppendLine("Top 10 words in all files together:");
            foreach (var word in topWords)
            {
                sb.AppendLine($"{word.Key}: {word.Value}");
            }

            time.Stop();
            txtBox2.Text = "Time: " + time.ElapsedMilliseconds.ToString() + " ms\n\n";
            txtBox2.Text += sb.ToString();
            Mouse.OverrideCursor = null;
        }
    }
}
