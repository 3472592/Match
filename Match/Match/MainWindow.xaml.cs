using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using System.Collections.Generic;
using System.Windows.Input;

namespace Match
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        TextBlock lastClicked;
        bool findingMatch = false;
        int tenthOfSecElapsed;
        int matchesFound;
        DispatcherTimer timer = new DispatcherTimer();

        public MainWindow()
        {
            InitializeComponent();
            timer.Interval = TimeSpan.FromSeconds(.1);
            timer.Tick += Timer_Tick;
            Set();
        }

        private void Set()
        {
            List<string> character = new List<string>() {
                "a","a",
                "b","b",
                "c","c",
                "d","d",
                "e","e",
                "f","f",
                "g","g",
                "h","h",
            };

            Random random = new Random();

            foreach (TextBlock textBlock in mainGrid.Children.OfType<TextBlock>()) {
                if(textBlock.Name != "timeTextBlock")
                {
                    textBlock.Visibility = Visibility.Visible;
                    int i = random.Next(character.Count);
                    string nextE = character[i];
                    textBlock.Text = nextE;
                    character.RemoveAt(i);
                }
            }
            timer.Start();
            tenthOfSecElapsed = 0;
            matchesFound = 0;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            tenthOfSecElapsed++;
            timeTextBlock.Text = (tenthOfSecElapsed / 10F).ToString("0.0s");
            if(matchesFound == 8)
            {
                timer.Stop();
                timeTextBlock.Text += " - Play again?";
            }
        }

        private void TimeTextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (matchesFound == 8)
            {
                Set();
            }
        }

        private void TextBlock_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            TextBlock textBlock = sender as TextBlock;
            if(findingMatch == false)
            {
                textBlock.Visibility = Visibility.Hidden;
                lastClicked = textBlock;
                findingMatch = true;
            } else if(textBlock.Text == lastClicked.Text)
            {
                matchesFound++;
                textBlock.Visibility = Visibility.Hidden;
                findingMatch = false;
            } else {
                lastClicked.Visibility = Visibility.Visible;
                findingMatch = false;
            }
        }
    }
}
