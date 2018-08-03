using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Collections.ObjectModel;
using System.Data;


namespace WpfDiplom
{
    /// <summary>
    /// Логика взаимодействия для Products.xaml
    /// </summary>
    public partial class Products : Page
    {
        public static StroitelEntities DataEntitiesProducts { get; set; }
        ObservableCollection<Товар> ListProducts;
        
        public Products()
        {
            DataEntitiesProducts = new StroitelEntities();
            InitializeComponent();
            ListProducts = new ObservableCollection<Товар>();
        }


        private void Page_LoadedProducts(object sender, RoutedEventArgs e)
        {
            ZagrProd();
            tbSt.Text = "ЗАГРУЖЕНО";
        }

        private void ZagrProd()
        {
            ListProducts.Clear();
            var products = DataEntitiesProducts.Товар;
            var queryProduct = from product in products
                               orderby product.Код_товара
                               select product;
            foreach (Товар product in queryProduct)
            {
                ListProducts.Add(product);
            }
            dgProducts.ItemsSource = ListProducts;
            tbCount.Text = Convert.ToString(ListProducts.Count());
            tbDate.Text = Convert.ToString(DateTime.Today.ToString("dd MMMM yyyy"));
        }

        private void clNewProduct(object sender, RoutedEventArgs e)
        {

            var newProd = new wNewProduct(this);
            newProd.Closed += newProd_Closed;
            newProd.ShowDialog();

        }

        void newProd_Closed(object sender, EventArgs e)
        {
            ZagrProd();
        }

        private void clDeleteProd(object sender, RoutedEventArgs e)
        {
            Товар prod = dgProducts.SelectedItem as Товар;
            if (prod != null)
            {
                MessageBoxResult result = MessageBox.Show("Удалить данные", "Предупреждение", MessageBoxButton.OKCancel, MessageBoxImage.Question);
                if (result == MessageBoxResult.OK)
                {
                    try
                    {
                        DataEntitiesProducts.Товар.Remove(prod);
                        DataEntitiesProducts.SaveChanges();

                        dgProducts.SelectedIndex = dgProducts.SelectedIndex == 0 ? 1 : dgProducts.SelectedIndex - 1;
                        ListProducts.Remove(prod);

                        tbCount.Text = Convert.ToString(ListProducts.Count());
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

        private void clSaveProd(object sender, RoutedEventArgs e)
        {
            DataEntitiesProducts.SaveChanges();
            dgProducts.IsReadOnly = true;
            tbSt.Text = "СОХРАНЕНО";
        }

        private void clEditProd(object sender, RoutedEventArgs e)
        {
            dgProducts.IsReadOnly = false;
            dgProducts.BeginEdit();
            tbSt.Text = "РЕДАКТИРУЕТСЯ";
        }

        private void clRefreshProd(object sender, RoutedEventArgs e)
        {
            ZagrProd();
        }

        private void clFindProd(object sender, RoutedEventArgs e)
        {
            
            string name = tbName.Text;

            DataEntitiesProducts = new StroitelEntities();
            ListProducts.Clear();

            var products = DataEntitiesProducts.St(name);
            var list = new List<St_Result>();

            list = (from item in products select item).ToList();

            if (list.Count > 0)
            {

                dgProducts.ItemsSource = list;
                tbCount.Text = Convert.ToString(list.Count());
            }
            else
            {
                MessageBox.Show("Товар с названием \n" + name + "\n не найден", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            
        }

        private void pdExit(object sender, RoutedEventArgs e)
        {
            Other.Exit();
        }

        private void newOtchNovinki(object sender, RoutedEventArgs e)
        {
            var _newOtchNovinki = new wOtchNovinki();
            _newOtchNovinki.ShowDialog();
        }
    }
}
