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

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            const int amountOfWords = 10;
            const string filepath = "D:\\repos\\CNET2\\bigFiles";
            var files = Directory.GetFiles(filepath, "*.*");
            var fileWordCounts = new Dictionary<string, List<KeyValuePair<string, int>>>(StringComparer.OrdinalIgnoreCase);

            foreach (var file in files)
            {
                var wordCount = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
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
            txtBox.Text = sb.ToString();
        }
    }
}
