using System.Windows;

namespace WPFclient
{
    public partial class MainWindow : Window
    {
        Students SM = new Students();
        public MainWindow()
        {
            InitializeComponent();
            DataContext = SM;
        }
    }
}
