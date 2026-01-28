using System.Collections.Concurrent;
using System.Diagnostics;
using System.Globalization;
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
using WpfApp;

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

        async Task<int> LoadFromFile()
        {
            const string filepath = "D:\\repos\\CNET2\\bigFiles";
            var files = Directory.GetFiles(filepath, "*.*");
            int count = 0;
            foreach ( var file in files )
            {
                var words = await File.ReadAllLinesAsync(file);
                count += words.Length;
            }
            txtBox1.Text += "Files loaded.\n";
            return count;
        }

        async private void btnByOne_Click(object sender, RoutedEventArgs e)
        {

            Mouse.OverrideCursor = Cursors.Wait;
            Stopwatch time = Stopwatch.StartNew();
            const int amountOfWords = 10;
            const string filepath = "D:\\repos\\CNET2\\bigFiles";
            var files = Directory.GetFiles(filepath, "*.*");

            var fileWordCounts = await FileProcesses.GetTopUsedWordsAsync(amountOfWords, files);

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

        

        async private void btnWhole_Click(object sender, RoutedEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Wait;
            Stopwatch time = Stopwatch.StartNew();
            const int amountOfWords = 10;
            const string filepath = "D:\\repos\\CNET2\\bigFiles";
            var files = Directory.GetFiles(filepath, "*.*");

            // Dictionary to count words across all files


            Dictionary<string, int> totalWordCount = await Task.Run(() => FileProcesses.GetTopWordsWhole(files));

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

        

        private void btnColour_Click(object sender, RoutedEventArgs e)
        {
            //here I want to randomly change the background colour of the window
            var rand = new Random();
            var color = Color.FromRgb((byte)rand.Next(256), (byte)rand.Next(256), (byte)rand.Next(256));
            this.Background = new SolidColorBrush(color);

        }

        async private void btnWholeAll_Click(object sender, RoutedEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Wait;
            const int amountOfWords = 10;
            const string filepath = "D:\\repos\\CNET2\\bigFiles";
            var files = Directory.GetFiles(filepath, "*.*");

            txtBox1.Text = "";
            txtBox2.Text = "";

            await foreach (var (byOne, whole) in FileProcesses.GetIncrementalStatsAsync(amountOfWords, files))
            {
                txtBox1.Text = byOne;
                txtBox2.Text = whole;
            }

            Mouse.OverrideCursor = null;
        }

        async private void btnWholeParallel_Click(object sender, RoutedEventArgs e)
        {
            /*
              10 most common words in all files globally
               USING PARALLEL method
           */
            const string filepath = "D:\\repos\\CNET2\\bigFiles";
            Mouse.OverrideCursor = System.Windows.Input.Cursors.Wait;

            Stopwatch time = new Stopwatch();
            time.Start();

            txtBox2.Text = "";

            List<string> files = Directory.EnumerateFiles(filepath, "*.txt")
                                         .ToList();

            ConcurrentDictionary<string, int> wordCount = new();

            //foreach (var file in files)
            Parallel.ForEach(files, file =>
            {
                var words = System.IO.File.ReadAllLines(file);

                foreach (var word in words)
                {
                    wordCount.AddOrUpdate(word, 1, (key, oldValue) => oldValue + 1);
                }
            });

            var top10 = wordCount.OrderByDescending(x => x.Value).Take(10);

            foreach (var item in top10)
            {
                txtBox2.Text += $"{item.Key} - {item.Value}{Environment.NewLine}";
            }
            txtBox2.Text += Environment.NewLine;

            time.Stop();
            txtBox2.Text += "Time: " + time.ElapsedMilliseconds.ToString() + " ms\n\n";

            Mouse.OverrideCursor = null;
        }

        private async void btnWholeParallelAsync_Click(object sender, RoutedEventArgs e)
        {
            /*
              10 most common words in all files globally
               USING PARALLEL method
           */
            const string filepath = "D:\\repos\\CNET2\\bigFiles";
            Mouse.OverrideCursor = System.Windows.Input.Cursors.Wait;

            Stopwatch time = new Stopwatch();
            time.Start();

            txtBox2.Text = "";

            List<string> files = Directory.EnumerateFiles(filepath, "*.txt")
                                         .ToList();

            ConcurrentDictionary<string, int> wordCount = new();

            //foreach (var file in files)
            await Parallel.ForEachAsync(files, async(file, cancellationToken) =>
            {
                var words = System.IO.File.ReadAllLines(file);

                foreach (var word in words)
                {
                    wordCount.AddOrUpdate(word, 1, (key, oldValue) => oldValue + 1);
                }
            });

            var top10 = wordCount.OrderByDescending(x => x.Value).Take(10);

            foreach (var item in top10)
            {
                txtBox2.Text += $"{item.Key} - {item.Value}{Environment.NewLine}";
            }
            txtBox2.Text += Environment.NewLine;

            time.Stop();
            txtBox2.Text += "Time: " + time.ElapsedMilliseconds.ToString() + " ms\n\n";

            Mouse.OverrideCursor = null;
        }
    }
}
