using System.Windows;
using IdeaManager.UI.Views;

namespace IdeaManager.UI
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void NavigToDashboard(object sender, RoutedEventArgs e)
        {
            GoDashboardButton.Visibility = Visibility.Collapsed;
            MainFrame2.Visibility = Visibility.Visible;
            MainFrame2.Navigate(new DashboardView());
        }
    }
}
