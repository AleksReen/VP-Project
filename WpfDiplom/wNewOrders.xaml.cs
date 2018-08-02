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
using System.Windows.Shapes;

namespace WpfDiplom
{
    /// <summary>
    /// Логика взаимодействия для wNewOrders.xaml
    /// </summary>
    public partial class wNewOrders : Window
    {
        public static StroitelEntities NewOrd { get; set; }
        public wNewOrders()
        {
            NewOrd = new StroitelEntities();
            InitializeComponent();
        }

        private void AddNewOrder(object sender, RoutedEventArgs e)
        {
            try
            {
                DateTime date = Convert.ToDateTime(dpDate.Text);
                decimal dostavka = Convert.ToDecimal(tbDostavka.Text);
                int kolichestvo = Convert.ToInt32(tbKolichestvo.Text);
                decimal skidka = Convert.ToDecimal(tbDiscont.Text);
                string oplata = tbOplata.Text;
                int cod_cl = Convert.ToInt32(cbClient.SelectedValue);
                int cod_emp = Convert.ToInt32(cbEmployeer.SelectedValue);
                int cod_prd = Convert.ToInt32(cbTovar.SelectedValue);

           
                MessageBoxResult result =
                        MessageBox.Show("Оформить новый заказ?", "Проверка данных", MessageBoxButton.OKCancel);
                if (result == MessageBoxResult.OK)
                {
                    NewOrd.Add_Zakaz_NEW(date, dostavka, kolichestvo, skidka, oplata, cod_cl, cod_emp, cod_prd);
                    MessageBox.Show("Новый заказ оформлен!", "Статус операции");

                    TbClear();
                }
            }
            catch
            {
                MessageBox.Show(" Добавление невозможно \n Проверьте заполнение полей!!!", "Ошибка добавления");
            }
        }

        private void ClearNewOrd(object sender, RoutedEventArgs e)
        {
            TbClear();
        }
        private void TbClear()
        {
            tbDiscont.Clear();
            tbDostavka.Clear();
            tbKolichestvo.Clear();
            tbOplata.Clear();
        }
    }
}
