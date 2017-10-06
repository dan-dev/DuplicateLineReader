using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace duplicatelinereader
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
            startBtn.Click += StartBtn_Click;
            fileDial.Click += FileDial_Click;
            paramsBox.GotFocus += paramsBox_GotFocus;
            paramsBox.LostFocus += paramsBox_LostFocus;
            paramsBox.Text = stockText;
        }

        string stockText = "Lines to ignore (ex: {;};Line1;Line2 )";

        private void paramsBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrWhiteSpace(paramsBox.Text))
            {
                paramsBox.Text = stockText;
            }
        }

        private void paramsBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if(paramsBox.Text == stockText)
            {
                paramsBox.Text = "";
            }
        }

        string fileway = "";

        private void FileDial_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if(openFileDialog.ShowDialog() == true)
            {
                fileway = openFileDialog.FileName;
            }
            fileLabel.Content = fileway;
        }

        private void StartBtn_Click(object sender, RoutedEventArgs e)
        {
            if(fileway == "")
            {
                MessageBox.Show("Select the file before starting.");
            }
            else
            {
                GetTextFile();
            }
        }

        void GetTextFile()
        {
            List<String> paramList = paramsBox.Text.Split(';').ToList();
            string[] text = System.IO.File.ReadAllLines(fileway);
            Dictionary<string, List<int>> duplit = new Dictionary<string, List<int>>();

            int i = 0;
            foreach (string line in text)
            {
                string s = line.Trim();

                var match = paramList.FirstOrDefault(x => x.Contains(s));

                if(match == null)
                {
                    if (duplit.ContainsKey(s))
                    {
                        List<int> linesList = duplit[s];
                        linesList.Add(i);
                    }
                    else
                    {
                        duplit.Add(s, new List<int> { i });
                    }
                }
                i++;
            }

            string result = "Results:\n";

            foreach (var item in duplit)
            {
                if(item.Value.Count > 1)
                {
                    result += item.Key + ": " + String.Join(",", item.Value.ToArray()) + "\n";
                }
            }

            resultBlock.Text = result;
        }
    }
}