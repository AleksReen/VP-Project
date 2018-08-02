using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Collections.ObjectModel;
using System.Data;
using System.Collections;

namespace WpfDiplom
{
    /// <summary>
    /// Логика взаимодействия для Orders.xaml
    /// </summary>
    public partial class Orders : Page
    {
        public static StroitelEntities DataEntitiesOrders { get; set; }
        ObservableCollection<Заказ> ListOrders;
        

        public Orders()
        {
            DataEntitiesOrders = new StroitelEntities();
            InitializeComponent();
            ListOrders = new ObservableCollection<Заказ>();
        }

        #region Оформление заказа

        private void clNewOrder(object sender, RoutedEventArgs e)
        {
            var newOrder = new wNewOrders();
            newOrder.Closed += newOrder_Closed;
            newOrder.ShowDialog();
        }
        void newOrder_Closed(object sender, EventArgs e)
        {
            ZagrOrders();
        }

        #endregion

        #region Старт страницы
        private void Page_LoadedOrders(object sender, RoutedEventArgs e)
        {
            ZagrOrders();
            tbSt.Text = "ЗАГРУЖЕНО";
        }
        #endregion

        #region загрузка заказов
        private void ZagrOrders()
        {
            ListOrders.Clear();
            var orders = DataEntitiesOrders.Заказ;
            var queryOrder = from order in orders
                             orderby order.Код_заказа
                             select order;
            foreach (Заказ order in queryOrder)
            {
                ListOrders.Add(order);
            }
            dgOrders.ItemsSource = ListOrders;
            tbCount.Text = Convert.ToString(ListOrders.Count());
            tbDate.Text = Convert.ToString(DateTime.Today.ToString("dd MMMM yyyy"));
        }
        #endregion

        #region Удаление Заказа
        private void clDeleteOrder(object sender, RoutedEventArgs e)
        {
            Заказ ord = dgOrders.SelectedItem as Заказ;
            if (ord != null)
            {
                MessageBoxResult result =
                    MessageBox.Show("Удалить данные о заказе?", "Предупреждение", MessageBoxButton.OKCancel, MessageBoxImage.Question);
                if (result == MessageBoxResult.OK)
                {
                    try
                    {
                        DataEntitiesOrders.Заказ.Remove(ord);
                        DataEntitiesOrders.SaveChanges();

                        dgOrders.SelectedIndex = dgOrders.SelectedIndex == 0 ? 1 : dgOrders.SelectedIndex - 1;
                        ListOrders.Remove(ord);

                        tbCount.Text = Convert.ToString(ListOrders.Count());
                        MessageBox.Show("Выбранная вами запись удалена!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    catch (Exception)
                    {

                        MessageBox.Show("Удаление не возможно", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                   
                }
            }
            else
            {
                MessageBox.Show("Выберите строку для удаления", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        #endregion

        #region Метод сохранения 
        private void clSaveOrder(object sender, RoutedEventArgs e)
        {
            DataEntitiesOrders.SaveChanges();
            dgOrders.IsReadOnly = true;
            tbSt.Text = "СОХРАНЕНО";
        }
        #endregion

        #region Метод редактирования
        private void clEditOrder(object sender, RoutedEventArgs e)
        {
            dgOrders.IsReadOnly = false;
            dgOrders.BeginEdit();
            tbSt.Text = "РЕДАКТИРУЕТСЯ";
        }
        #endregion

        #region Метод обновления
        private void clRefreshOrders(object sender, RoutedEventArgs e)
        {
            ZagrOrders();
        }
        #endregion

        #region Метод выход из программы
        private void clExit(object sender, RoutedEventArgs e)
        {
            Other.Exit();
        }
        #endregion

        #region Поиск 
        private void clFindOrder(object sender, RoutedEventArgs e)
        {

            #region Поиск по фамилии клиента
            string org = tbFamily.Text;
            DataEntitiesOrders = new StroitelEntities();
            ListOrders.Clear();
            ArrayList SerchListFamily = new ArrayList();
            var orders = DataEntitiesOrders.Заказ;
            var client = DataEntitiesOrders.Клиент;

            var queryClient = from ord in orders
                              join cl in client
                              on ord.Код_клиента equals cl.Код_клиента
                              where cl.Организация.Contains(org)
                              select ord;
                              
            foreach (var or in queryClient)
            {
                SerchListFamily.Add(or);
            }
            if (SerchListFamily.Count > 0)
            {
                dgOrders.ItemsSource = SerchListFamily;

                tbCount.Text = Convert.ToString(SerchListFamily.Count);
            }
            else
                MessageBox.Show("Заказы клиента \n" + org + "\n не найден",
                     "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
            #endregion

            #region Поиск по номеру заказа
            //int org = Convert.ToInt32(tbFamily.Text);
            //DataEntitiesOrders = new StroitelEntities();
            //ListOrders.Clear();
            //var orders = DataEntitiesOrders.Заказ;
            //var queryClient = from ord in orders
            //                  where ord.Код_заказа == org
            //                  select ord;
            //foreach (Заказ or in queryClient)
            //{
            //    ListOrders.Add(or);
            //}
            //if (ListOrders.Count > 0)
            //{
            //   dgOrders.ItemsSource = ListOrders;
            //    tbCount.Text = Convert.ToString(ListOrders.Count());
            //}
            //else
            //    MessageBox.Show("Заказ клиента \n" + org + "\n не найден",
            //         "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
            #endregion
        }
        #endregion

        #region Работа магазина за период

        private void OtchRabMagPeriod(object sender, RoutedEventArgs e)
        {
            var _newOtchRabMagPeriod = new wOtchRabMagPeriod();
            _newOtchRabMagPeriod.ShowDialog();
        }

        #endregion

    }
}
