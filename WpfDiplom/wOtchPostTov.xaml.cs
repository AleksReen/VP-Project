using System;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.IO;
using Microsoft.Win32;

namespace WpfDiplom
{
    /// <summary>
    /// Логика взаимодействия для wOtchPostTov.xaml
    /// </summary>
    public partial class wOtchPostTov : Window
    {
        private readonly StroitelEntities OtchPostTov;
        public wOtchPostTov()
        {
            InitializeComponent();
            OtchPostTov = new StroitelEntities();
            tbSt.Text = "Сформируйте отчет";
            tbCount.Text = "0";
            tbDate.Text = Convert.ToString(DateTime.Today.ToString("dd MMMM yyyy"));
        }

        private void Start(object sender, RoutedEventArgs e)
        {
            int post = Convert.ToInt32(cb.SelectedValue);
            if (post != 0)
            {
                DG.DataContext = OtchPostTov.Поставщик_товар(post);
                tbSt.Text = "Отчет сформирован";
                tbCount.Text = OtchPostTov.Поставщик_товар(post).Count().ToString();
            }
            else
            {
                MessageBox.Show("Проверьте заполнение полей!!!", "Ошибка");
            }
        }

        private void Print(object sender, RoutedEventArgs e)
        {
            Other.Prints(DG);
        }
       
        private void Export(object sender, RoutedEventArgs e)
        {
            string name = "Отчет Поставщик - товар ";
            Other.Exports(DG, name);
        }

    }
}
