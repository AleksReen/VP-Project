using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Data;
using System.Collections.Generic;

namespace VPproject
{
    public partial class Clients : Page 
    {
        public static StroitelEntities dbContext { get; set; }
        public List<Клиент> ListClients { get; set; }

        public Clients()
        {
            dbContext = new StroitelEntities();
            InitializeComponent();
            ListClients = new List<Клиент>();          
        }

        private void Page_LoadedClients(object sender, RoutedEventArgs e)
        {
            GetData();
            tbDate.Text = Convert.ToString(DateTime.Today.ToString("dd MMMM yyyy"));
            tbSt.Text = "ЗАГРУЖЕНО";
        }

        private void GetData()
        {
            if (ListClients.Any())
            {
                ListClients.Clear();
            }

            ListClients = dbContext.Клиент.OrderBy(cl => cl.Организация).ToList();

            dgClients.ItemsSource = ListClients;
            tbCount.Text = Convert.ToString(ListClients.Count());          
        }

        private void clNewClient(object sender, RoutedEventArgs e)
        {
            var newClient = new wNewClient(this);
            newClient.Closed += newClient_Closed;
            newClient.ShowDialog();
        }

        void newClient_Closed(object sender, EventArgs e)
        {
            GetData();
        }

        private void clDeleteClient(object sender, RoutedEventArgs e)
        {
            Клиент cl = dgClients.SelectedItem as Клиент;

            if (cl != null)
            {
                MessageBoxResult result = MessageBox.Show("Удалить данные", "Предупреждение", MessageBoxButton.OKCancel, MessageBoxImage.Question);
                if (result == MessageBoxResult.OK)
                {
                    try
                    {
                        dbContext.Клиент.Remove(cl);
                        dbContext.SaveChanges();

                        GetData();

                        MessageBox.Show("Выбранная вами запись удалена!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Удаление не возможно, есть связанные данные с этой записью", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Выберите строку для удаления", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void clSaveClient(object sender, RoutedEventArgs e)
        {
            dbContext.SaveChanges();
            dgClients.IsReadOnly = true;
            tbSt.Text = "СОХРАНЕНО";
        }

        private void clEditClient(object sender, RoutedEventArgs e)
        {
            dgClients.BeginEdit();
            dgClients.IsReadOnly = false;
            tbSt.Text = "РЕДАКТИРУЕТСЯ";
        }

        private void clRefreshClient(object sender, RoutedEventArgs e)
        {
            GetData();
        }

        private void clFindClient(object sender, RoutedEventArgs e)
        {
            var org = tbOrg.Text;

            if (ListClients.Any())
            {
                ListClients.Clear();
            }
           
            ListClients = dbContext.Клиент.Where(c => c.Организация.Contains(org)).ToList();
                              
            if (ListClients.Count > 0)
            {
                dgClients.ItemsSource = ListClients;
                tbCount.Text = Convert.ToString(ListClients.Count());
            }
            else
            {
                MessageBox.Show("Организация клиента \n" + org + "\n не найдена", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
            }       
        }

        private void clExit(object sender, RoutedEventArgs e)
        {
            Other.Exit();
        }

        private void wOtchClientMogilev(object sender, RoutedEventArgs e)
        {
            var newOtchClientMogilev = new wOtchClientMogilev();
            newOtchClientMogilev.Show();
        }
    }
}
