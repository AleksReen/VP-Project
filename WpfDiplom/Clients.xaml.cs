using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Collections.ObjectModel;
using System.Data;


namespace WpfDiplom
{
    public partial class Clients : Page 
    {
        public static StroitelEntities DataEntitiesClients { get; set; }
        private ObservableCollection<Клиент> ListClients;

        public Clients()
        {
            DataEntitiesClients = new StroitelEntities();
            InitializeComponent();
            ListClients = new ObservableCollection<Клиент>();
        }

        private void Page_LoadedClients(object sender, RoutedEventArgs e)
        {
            GetData();
            tbSt.Text = "ЗАГРУЖЕНО";
        }

        private void GetData()
        {
            ListClients.Clear();
            var clients = DataEntitiesClients.Клиент;
            var queryClient = from client in clients
                              orderby client.Код_клиента
                              select client;
            foreach (Клиент client in queryClient)
            {
                ListClients.Add(client);
            }
            dgClients.ItemsSource = ListClients;
            tbCount.Text = Convert.ToString(ListClients.Count());
            tbDate.Text = Convert.ToString(DateTime.Today.ToString("dd MMMM yyyy"));
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
                        DataEntitiesClients.Клиент.Remove(cl);
                        DataEntitiesClients.SaveChanges();

                        dgClients.SelectedIndex = dgClients.SelectedIndex == 0 ? 1 : dgClients.SelectedIndex - 1;
                        ListClients.Remove(cl);
                        tbCount.Text = Convert.ToString(ListClients.Count());
                        MessageBox.Show("Выбранная вами запись удалена!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    catch (Exception)
                    {

                        MessageBox.Show("Удаление невозможно, клиент имеет связанные записи", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Error);
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
            DataEntitiesClients.SaveChanges();
            dgClients.IsReadOnly = true;
            tbSt.Text = "СОХРАНЕНО";
        }

        private void clEditClient(object sender, RoutedEventArgs e)
        {
            dgClients.IsReadOnly = false;
            dgClients.BeginEdit();
            tbSt.Text = "РЕДАКТИРУЕТСЯ";
        }

        private void clRefreshClient(object sender, RoutedEventArgs e)
        {
            GetData();
        }

        private void clFindClient(object sender, RoutedEventArgs e)
        {
            string org = tbOrg.Text;
            DataEntitiesClients = new StroitelEntities();
            ListClients.Clear();
            var clients = DataEntitiesClients.Клиент;
            var queryClient = from client in clients
                              where client.Организация == org 
                              select client;
            foreach (Клиент cl in queryClient)
            {
                ListClients.Add(cl);
            }
            if (ListClients.Count > 0)
            {
                dgClients.ItemsSource = ListClients;
                tbCount.Text = Convert.ToString(ListClients.Count());
            }
            else
                MessageBox.Show("Организация клиента \n" + org + "\n не найдена", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
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
