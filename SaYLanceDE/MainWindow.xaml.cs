﻿using Microsoft.Win32;
using SaYLance;
using SaYLanceDE.src;
using System.IO;
using System.Threading;
using System.Windows;
namespace SaYLanceDE
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            richTextBox.IsReadOnly = true;

        }

        private void RunButtonClick(object sender, RoutedEventArgs e)
        {
            string filePath = "";
            TextField textField = new(richTextBox);
            try
            {
                filePath = filePathInput.Text;
                if (!File.Exists(filePath)) throw new FileNotFoundException($"File not found at path: {filePath}");
            }
            catch (Exception ex)
            {
                textField.Error(ex.Message);
                return;
            }

            richTextBox.Document.Blocks.Clear();
            new Thread(() =>
                {
                    Thread.CurrentThread.IsBackground = false;
                    Interpreter interpreter = new(textField);
                    interpreter.Run(filePath);
                }).Start();

        }
        private void BrowseFileClick(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
                filePathInput.Text = openFileDialog.FileName;
        }
    }
}