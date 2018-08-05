using System;
using System.Windows;

namespace VPproject
{
    public partial class wNewOrders : Window
    {
        public static StroitelEntities dbContext { get; set; }
        public wNewOrders()
        {
            dbContext = new StroitelEntities();
            InitializeComponent();
        }

        private void AddNewOrder(object sender, RoutedEventArgs e)
        {
            try
            {
                DateTime date = Convert.ToDateTime(dpDate.Text);               
                string oplata = tbOplata.Text;

                int cod_cl = Convert.ToInt32(cbClient.SelectedValue);
                int cod_emp = Convert.ToInt32(cbEmployeer.SelectedValue);
                int cod_prd = Convert.ToInt32(cbTovar.SelectedValue);

                var dostavka = tbDostavka.Text.Trim();
                
                var skidka = tbDiscont.Text.Trim();

                if (tbDostavka.Text.Contains("."))
                {
                    dostavka = tbDostavka.Text.Replace(".", ",");
                }

                if (tbDiscont.Text.Contains("."))
                {
                    skidka = tbDiscont.Text.Replace(".", ",");
                }


                decimal dostavkaOrder = Convert.ToDecimal(dostavka);
                decimal skidkaOrder = Convert.ToDecimal(skidka);

                int kolichestvo = Convert.ToInt32(tbKolichestvo.Text.Trim());
                

                MessageBoxResult result = MessageBox.Show("Оформить новый заказ?", "Проверка данных", MessageBoxButton.OKCancel, MessageBoxImage.Question);

                if (result == MessageBoxResult.OK)
                {
                    try
                    {
                        dbContext.Add_Zakaz_NEW(date, dostavkaOrder, kolichestvo, skidkaOrder, oplata, cod_cl, cod_emp, cod_prd);
                        Clear();

                        MessageBox.Show("Новый заказ оформлен!", "Статус операции", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    catch (Exception)
                    {
                        MessageBox.Show(" Добавление невозможно \n Ошибка базы данных!", "Ошибка добавления", MessageBoxButton.OK, MessageBoxImage.Error);
                    }                
                }
            }
            catch
            {
                MessageBox.Show(" Добавление невозможно \n Проверьте заполнение полей!", "Ошибка добавления", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ClearNewOrd(object sender, RoutedEventArgs e)
        {
            Clear();
        }
        private void Clear()
        {
            tbDiscont.Clear();
            tbDostavka.Clear();
            tbKolichestvo.Clear();
            tbOplata.Clear();
            dpDate.DisplayDate = DateTime.Today;
            cbEmployeer.SelectedIndex = -1;
            cbClient.SelectedIndex = -1;
            cbTovar.SelectedIndex = -1;
        }

        private void tbKolichestvo_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0))
            {
                e.Handled = true;
            }
        }

        private void tbDiscont_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            if (!(Char.IsDigit(e.Text, 0) || (e.Text == ".") && (!tbDiscont.Text.Contains(".") && tbDiscont.Text.Length != 0)))
            {
                e.Handled = true;
            }
        }

        private void tbDostavka_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            if (!(Char.IsDigit(e.Text, 0) || (e.Text == ".") && (!tbDostavka.Text.Contains(".") && tbDostavka.Text.Length != 0)))
            {
                e.Handled = true;
            }
        }
    }
}
