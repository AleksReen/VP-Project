using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Data;
using System.Collections.Generic;

namespace VPproject
{
    public partial class Orders : Page
    {
        public static StroitelEntities dbContext { get; set; }
        private List<Заказ> ListOrders;
        
        public Orders()
        {
            dbContext = new StroitelEntities();
            InitializeComponent();
            ListOrders = new List<Заказ>();
        }

        private void clNewOrder(object sender, RoutedEventArgs e)
        {
            var newOrder = new wNewOrders();
            newOrder.Closed += newOrder_Closed;
            newOrder.ShowDialog();
        }

        void newOrder_Closed(object sender, EventArgs e)
        {
            GetData();
        }

        private void Page_LoadedOrders(object sender, RoutedEventArgs e)
        {
            GetData();
            tbDate.Text = Convert.ToString(DateTime.Today.ToString("dd MMMM yyyy"));
            tbSt.Text = "ЗАГРУЖЕНО";
        }

        private void GetData()
        {
            if (ListOrders.Any())
            {
                ListOrders.Clear();
            }

            ListOrders = dbContext.Заказ.OrderBy(o => o.Код_заказа).ToList();
         
            dgOrders.ItemsSource = ListOrders;
            tbCount.Text = Convert.ToString(ListOrders.Count());          
        }

        private void clDeleteOrder(object sender, RoutedEventArgs e)
        {
            Заказ ord = dgOrders.SelectedItem as Заказ;

            if (ord != null)
            {
                MessageBoxResult result = MessageBox.Show("Удалить данные о заказе?", "Предупреждение", MessageBoxButton.OKCancel, MessageBoxImage.Question);

                if (result == MessageBoxResult.OK)
                {
                    try
                    {
                        dbContext.Заказ.Remove(ord);
                        dbContext.SaveChanges();

                        GetData();

                        MessageBox.Show("Выбранная запись удалена!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Information);
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

        private void clSaveOrder(object sender, RoutedEventArgs e)
        {
            dbContext.SaveChanges();
            dgOrders.IsReadOnly = true;
            tbSt.Text = "СОХРАНЕНО";
        }

        private void clEditOrder(object sender, RoutedEventArgs e)
        {          
            dgOrders.BeginEdit();
            dgOrders.IsReadOnly = false;
            tbSt.Text = "РЕДАКТИРУЕТСЯ";
        }

        private void clRefreshOrders(object sender, RoutedEventArgs e)
        {
            GetData();
        }

        private void clExit(object sender, RoutedEventArgs e)
        {
            Other.Exit();
        }
 
        private void clFindOrder(object sender, RoutedEventArgs e)
        {
            string org = tbFamily.Text;

            if (ListOrders.Any())
            {
                ListOrders.Clear();
            }

            ListOrders = dbContext.Заказ.Join( 
                dbContext.Клиент, 
                o => o.Код_клиента,
                c => c.Код_клиента,
                (o, c) => new
                {
                    Order = o,
                    Client = c
                }).Where(r => r.Client.Организация.Contains(org)).Select( r => r.Order).ToList<Заказ>();
                              
           
            if (ListOrders.Count > 0)
            {
                dgOrders.ItemsSource = ListOrders;
                tbCount.Text = Convert.ToString(ListOrders.Count);
            }
            else
            {
                MessageBox.Show("Заказы \n" + org + "\n не найден", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Information);
            }             
        }

        private void OtchRabMagPeriod(object sender, RoutedEventArgs e)
        {
            var _newOtchRabMagPeriod = new wOtchRabMagPeriod();
            _newOtchRabMagPeriod.ShowDialog();
        }
    }
}
