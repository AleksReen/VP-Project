using System;
using System.Windows;

namespace VPproject
{
    /// <summary>
    /// Логика взаимодействия для wNewProduct.xaml
    /// </summary>
    public partial class wNewProduct : Window
    {
        public static StroitelEntities NewPr { get; set; }
        public wNewProduct(Products newPr)
        {
            NewPr = new StroitelEntities();
            InitializeComponent();
        }
        private void AddNewProd(object sender, RoutedEventArgs e)
        {
                try
                {
                    string name = tbNameProd.Text;
                    string proizv = tbProProduct.Text;
                    string ed = tbEdIzmProduct.Text;
                    int balance = Convert.ToInt32(tbBalanceProd.Text);
                    decimal price = Convert.ToDecimal(tbPrice.Text);
                    int prov = Convert.ToInt32(cbProvProduct.SelectedValue);
                    string n = tbNewProd.Text;
                            MessageBoxResult result =
                            MessageBox.Show("Добавить новый товар?", "Проверка данных", MessageBoxButton.OKCancel);
                    if (result == MessageBoxResult.OK)
                    {
                        NewPr.Add_New_Product(name, proizv, ed, balance, price, prov, n);
                        MessageBox.Show("Товар добавлен!", "Статус операции");
                        TbClear();
                    }
                }
                catch
                {
                    MessageBox.Show(" Добавление невозможно,\n проверьте заполнение полей!!!", "Ошибка добавления");
                }

        }
        private void ClearNewProd(object sender, RoutedEventArgs e)
        {
            TbClear();
        }
        private void TbClear()
        {
            tbNameProd.Clear();
            tbProProduct.Clear();
            tbEdIzmProduct.Clear();
            tbBalanceProd.Clear();
            tbPrice.Clear();
            tbNewProd.Clear();
        }
    }
}

