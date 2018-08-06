using System;
using System.Windows;
using VPproject.Models;

namespace VPproject
{  
    public partial class wNewProduct : Window
    {
        public static StroitelEntities dbContext { get; set; }
        public bool IsValid { get; set; } = true;

        public wNewProduct(Products newPr)
        {
            dbContext = new StroitelEntities();
            InitializeComponent();
            this.DataContext = new ProductValidationModel();
        }
        private void AddNewProd(object sender, RoutedEventArgs e)
        {
            try
            {
                string name = tbNameProd.Text;
                string proizv = tbProProduct.Text;
                string ed = tbEdIzmProduct.Text;
                int balance = Convert.ToInt32(tbBalanceProd.Text.Trim());

                var price = tbPrice.Text.Trim();

                if (tbPrice.Text.Contains("."))
                {
                    price = tbPrice.Text.Replace(".", ",");
                }
                
                decimal productPrice = Convert.ToDecimal(price);

                int prov = Convert.ToInt32(cbProvProduct.SelectedValue);
                string n = tbNewProd.Text;

                MessageBoxResult result = MessageBox.Show("Добавить новый товар?", "Проверка данных", MessageBoxButton.OKCancel, MessageBoxImage.Question);

                if (result == MessageBoxResult.OK)
                {
                    try
                    {
                        dbContext.Add_New_Product(name, proizv, ed, balance, productPrice, prov, n);
                        Clear();
                        MessageBox.Show("Товар добавлен!", "Статус операции", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    catch (Exception)
                    {
                        MessageBox.Show(" Добавление невозможно \n Ошибка базы данных!", "Ошибка добавления", MessageBoxButton.OK, MessageBoxImage.Error);
                    }                   
                }
            }
            catch
            {
                MessageBox.Show(" Добавление невозможно,\n проверьте заполнение полей!", "Ошибка добавления", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void ClearNewProd(object sender, RoutedEventArgs e)
        {
            Clear();
        }
        private void Clear()
        {
            tbNameProd.Clear();
            tbProProduct.Clear();
            tbEdIzmProduct.Clear();
            tbBalanceProd.Clear();
            tbPrice.Clear();
            cbProvProduct.SelectedIndex = -1;
            tbNewProd.SelectedIndex = -1;
        }

        private void tbBalanceProd_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0))
            {
                e.Handled = true;
            }
        }

        private void tbPrice_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            if (!(Char.IsDigit(e.Text, 0) || (e.Text == ".") && (!tbPrice.Text.Contains(".") && tbPrice.Text.Length != 0)))
            {
                e.Handled = true;
            }
        }
    }
}

