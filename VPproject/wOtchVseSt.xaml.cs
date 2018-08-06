using System;
using System.Linq;
using System.Windows;

namespace VPproject
{
 
    public partial class wOtchVseSt : Window
    {
        private StroitelEntities dbContext { get; set; }
        public wOtchVseSt()
        {
            InitializeComponent();
            dbContext = new StroitelEntities();
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

                DG.DataContext = dbContext.Otchet_po_vsem_sotr_period(N, K);

                tbCount.Text = dbContext.Otchet_po_vsem_sotr_period(N, K).Count().ToString();
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
            string name = "Все сотрудники за период ";
            Other.Exports(DG, name);
        }
    }
}
