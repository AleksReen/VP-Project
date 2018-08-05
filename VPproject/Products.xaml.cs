using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Data;

namespace VPproject
{
    public partial class Products : Page
    {
        public static StroitelEntities dbContext { get; set; }
        private List<Товар> ListProducts { get; set; }
        
        public Products()
        {
            dbContext = new StroitelEntities();
            InitializeComponent();
            ListProducts = new List<Товар>();
        }

        private void Page_LoadedProducts(object sender, RoutedEventArgs e)
        {
            GetData();
            tbDate.Text = Convert.ToString(DateTime.Today.ToString("dd MMMM yyyy"));
            tbSt.Text = "ЗАГРУЖЕНО";
        }

        private void GetData()
        {
            if (ListProducts.Any())
            {
                ListProducts.Clear();
            }

            ListProducts = dbContext.Товар.OrderBy(p => p.Код_товара).ToList();
          
            dgProducts.ItemsSource = ListProducts;
            tbCount.Text = Convert.ToString(ListProducts.Count());          
        }

        private void clNewProduct(object sender, RoutedEventArgs e)
        {
            var newProd = new wNewProduct(this);
            newProd.Closed += newProd_Closed;
            newProd.ShowDialog();
        }

        void newProd_Closed(object sender, EventArgs e)
        {
            GetData();
        }

        private void clDeleteProd(object sender, RoutedEventArgs e)
        {
            Товар prod = dgProducts.SelectedItem as Товар;

            if (prod != null)
            {
                MessageBoxResult result = MessageBox.Show("Удалить данные?", "Предупреждение", MessageBoxButton.OKCancel, MessageBoxImage.Question);
                if (result == MessageBoxResult.OK)
                {
                    try
                    {
                        dbContext.Товар.Remove(prod);
                        dbContext.SaveChanges();

                        GetData();

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
            dbContext.SaveChanges();
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
            GetData();
        }

        private void clFindProd(object sender, RoutedEventArgs e)
        {          
            string name = tbName.Text;

            if (ListProducts.Any())
            {
                ListProducts.Clear();
            }

            ListProducts = dbContext.Товар.Where(c => c.Наименование_товара.Contains(name)).ToList();                    

            if (ListProducts.Count > 0)
            {
                dgProducts.ItemsSource = ListProducts;
                tbCount.Text = Convert.ToString(ListProducts.Count());
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
