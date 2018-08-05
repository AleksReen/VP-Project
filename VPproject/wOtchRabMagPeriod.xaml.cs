using System;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.IO;
using Microsoft.Win32;

namespace VPproject
{
    /// <summary>
    /// Логика взаимодействия для wOtchRabMagPeriod.xaml
    /// </summary>
    public partial class wOtchRabMagPeriod : Window
    {
        private readonly StroitelEntities RabMagPeriod;
        private DateTime N { get; set; }
        private DateTime K { get; set; }
        public wOtchRabMagPeriod()
        {
            InitializeComponent();
            RabMagPeriod = new StroitelEntities();
            tbSt.Text = "Сформируйте отчет";
            tbCount.Text = "0";
            tbDate.Text = Convert.ToString(DateTime.Today.ToString("dd MMMM yyyy"));
        }
        private void Start(object sender, RoutedEventArgs e)
        {
            try
            {
                N = Convert.ToDateTime(dpDateN.Text);
                K = Convert.ToDateTime(dpDateK.Text);

                DG.DataContext = RabMagPeriod.Работа_магазина_за_период(N, K);

                tbCount.Text = RabMagPeriod.Работа_магазина_за_период(N, K).Count().ToString();
                tbSt.Text = "Cформирован";
            }
            catch
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
            string name = "Работа магазина за период ";
            Other.Exports(DG, name);
        }
    }
}
