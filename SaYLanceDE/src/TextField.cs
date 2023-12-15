
using SaYLance.interfaces;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

namespace SaYLanceDE.src
{
    public partial class TextField : IDefaultTextIO
    {
        private RichTextBox _textBox;
        private TaskCompletionSource<string> _inputCompletionSource;

        public TextField(RichTextBox richTextBox)
        {
            _textBox = richTextBox;
            _textBox.IsReadOnly = true;
            _textBox.PreviewKeyDown += TextBoxOnPreviewKeyDown;
        }

        private void TextBoxOnPreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (_textBox.IsReadOnly)
                return;

            if (e.Key == Key.Enter && Keyboard.Modifiers != ModifierKeys.Shift)
            {
                e.Handled = true;

                TextRange textRange = new TextRange(_textBox.Document.ContentStart, _textBox.Document.ContentEnd);
                string text = textRange.Text.Trim();

                _textBox.IsReadOnly = true;
                _inputCompletionSource.SetResult(text);
            }
        }

        public Task<string> StringInputAsync()
        {
            _inputCompletionSource = new TaskCompletionSource<string>();
            _textBox.Document.Blocks.Clear();
            _textBox.IsReadOnly = false;
            return _inputCompletionSource.Task;
        }

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
