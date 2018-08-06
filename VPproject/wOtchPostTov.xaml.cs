using System;
using System.Linq;
using System.Windows;

namespace VPproject
{
    public partial class wOtchPostTov : Window
    {
        private readonly StroitelEntities dbContext;
        public wOtchPostTov()
        {
            InitializeComponent();
            dbContext = new StroitelEntities();
            tbSt.Text = "Сформируйте отчет";
            tbCount.Text = "0";
            tbDate.Text = Convert.ToString(DateTime.Today.ToString("dd MMMM yyyy"));
        }

        private void Start(object sender, RoutedEventArgs e)
        {
            int post = Convert.ToInt32(cb.SelectedValue);

            if (post != 0)
            {
                DG.DataContext = dbContext.Поставщик_товар(post);
                tbSt.Text = "Cформирован";
                tbCount.Text = dbContext.Поставщик_товар(post).Count().ToString();
            }
            else
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
            string name = "Отчет Поставщик-Tовар ";
            Other.Exports(DG, name);
        }

    }
}
