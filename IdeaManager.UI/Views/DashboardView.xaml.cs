using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace IdeaManager.UI.Views
{

    public partial class DashboardView : Page
    {
        public DashboardView()
        {
            InitializeComponent();
        }

        private void NavigToForm(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new IdeaFormView());
        }


        private void NavigToList(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new IdeaListView());
        }
    }
}