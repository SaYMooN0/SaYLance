using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace SaYLanceDE.src
{
    public partial class TextField : SaYLance.interfaces.IDefaultTextIO
    {
        private RichTextBox _textBox;

        public TextField(RichTextBox richTextBox) { _textBox = richTextBox; }
        private void WriteText(string text, Brush textColor)
        {
            var paragraph = new Paragraph(new Run(text))
            {
                Foreground = textColor,
                LineHeight = 1.2,
                Margin = new Thickness(0)
            };
            _textBox.Document.Blocks.Add(paragraph);
        }

        public void AllowInput()
        {
            _textBox.IsReadOnly =true;
        }

        public void Clear()
        {
            _textBox.Document.Blocks.Clear();
        }
        public void Error(string error)
        {
            WriteText("Error: " + error, Brushes.Red);
        }

        public void Log(string message)
        {
            WriteText(message, Brushes.Black);
        }

        public void Warning(string warning)
        {
            WriteText("Warning: " + warning, Brushes.Orange);
        }

    }
}
