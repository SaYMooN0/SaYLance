using SaYLance;
using System.Reflection.Metadata;
using System.Windows;
namespace SaYLanceDE
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Interpreter.Run("str");
        }

        private void RunButtonClick(object sender, RoutedEventArgs e)
        {
            
            try
            {
                string inputValue = inputTextBox.Text;
            }
            catch (Exception ex)
            {
             
            }
        }
    }
}