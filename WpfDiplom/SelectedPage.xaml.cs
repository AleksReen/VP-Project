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

namespace WpfDiplom
{
    /// <summary>
    /// Логика взаимодействия для SelectedPage.xaml
    /// </summary>
    public partial class SelectedPage : Page
    {
        public SelectedPage()
        {
            InitializeComponent();
        }

        private void btnProducts_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("Products.xaml", UriKind.Relative));
        }

        private void btnOrders_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("Orders.xaml", UriKind.Relative));
        }

        private void btnClients_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("Clients.xaml", UriKind.Relative));
        }

        private void btnProviders_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("Providers.xaml", UriKind.Relative));
        }

        private void btnRemonts_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("Remonts.xaml", UriKind.Relative));
        }

        private void btnEmployees_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("Employees.xaml", UriKind.Relative));
        }
    }
}
