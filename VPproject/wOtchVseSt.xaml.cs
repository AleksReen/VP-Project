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
    /// Логика взаимодействия для wOtchVseSt.xaml
    /// </summary>
    public partial class wOtchVseSt : Window
    {
        private readonly StroitelEntities OtchOtchVseSt;
        private DateTime N { get; set; }
        private DateTime K { get; set; }
        public wOtchVseSt()
        {
            InitializeComponent();
            OtchOtchVseSt = new StroitelEntities();
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

                DG.DataContext = OtchOtchVseSt.Otchet_po_vsem_sotr_period(N, K);

                tbCount.Text = OtchOtchVseSt.Otchet_po_vsem_sotr_period(N, K).Count().ToString();
                tbSt.Text = "Отчет сформирован";
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
            string name = "Все сотрудники за период ";
            Other.Exports(DG, name);
        }
    }
}
