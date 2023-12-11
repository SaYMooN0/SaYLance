using SaYLance;
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
    }
}