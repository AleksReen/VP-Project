using System;
using System.Linq;
using System.Windows;

namespace VPproject
{
    public partial class wOtchRabMagPeriod : Window
    {
        private readonly StroitelEntities dbContex;
       
        public wOtchRabMagPeriod()
        {
            dbContex = new StroitelEntities();
            InitializeComponent();

            tbSt.Text = "Сформируйте отчет";
            tbCount.Text = "0";
            tbDate.Text = Convert.ToString(DateTime.Today.ToString("dd MMMM yyyy"));
        }

        private void Start(object sender, RoutedEventArgs e)
        {
            try
            {
                var N = Convert.ToDateTime(dpDateN.Text);
                var K = Convert.ToDateTime(dpDateK.Text);

                DG.DataContext = dbContex.Работа_магазина_за_период(N, K);

                tbCount.Text = dbContex.Работа_магазина_за_период(N, K).Count().ToString();
                tbSt.Text = "Cформирован";
            }
            catch
            {
                MessageBox.Show("Проверьте заполнение полей!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
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
