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
    /// Логика взаимодействия для wNewRemont.xaml
    /// </summary>
    public partial class wNewRemont : Window
    {
        public static StroitelEntities NewRm { get; set; }

        public wNewRemont()
        {
            NewRm = new StroitelEntities();
            InitializeComponent();
        }

        private void AddNewRem(object sender, RoutedEventArgs e)
        {
            int cl = Convert.ToInt32(cbCl.SelectedValue);
            int pr = Convert.ToInt32(cbPr.SelectedValue);

            try
            {
                MessageBoxResult result =
                        MessageBox.Show("Оформить новый ремонт?", "Проверка данных", MessageBoxButton.OKCancel);
                if (result == MessageBoxResult.OK)
                {
                    NewRm.ADD_NEW_REMONT(cl, pr);
                    MessageBox.Show("Новый ремонт оформлен!", "Статус операции");
                }
            }
            catch
            {
                MessageBox.Show(" Добавление невозможно \n Проверьте заполнение полей!!!", "Ошибка добавления");
            }
        }
    }
}
