using System.Windows.Controls;

namespace SaYLanceDE.src
{
    public partial class TextField:SaYLance.interfaces.IDefaultTextIO
    {
        private RichTextBox _richTextBox;

        public TextField(RichTextBox richTextBox) { _richTextBox = richTextBox; }

        public void AllowInput() { }
        public void Clear(){}
        public void Error(string error) { }
        public void Warning(string warning) {  }
    }
}
