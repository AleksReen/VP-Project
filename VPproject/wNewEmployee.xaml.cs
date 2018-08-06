using System.Windows;
using WpfDiplom.Models;

namespace VPproject
{
    public partial class wNewEmployee : Window
    {
        public static StroitelEntities dbContext { get; set; }

        public wNewEmployee(Employees newEmp)
        {
            dbContext = new StroitelEntities();
            InitializeComponent();
            this.DataContext = new EmployerValidationModel();            
        }
        private void AddNewEmployee(object sender, RoutedEventArgs e)
        {
            string f = tbFamEmp.Text;
            string i = tbNameEmp.Text;
            string p = tbPatEmp.Text;
            string t = tbTitEmp.Text;
            string tel = tbTelEmp.Text;

            if (!string.IsNullOrEmpty(f)
                && !string.IsNullOrEmpty(i)
                && !string.IsNullOrEmpty(p)
                && !string.IsNullOrEmpty(t)
                && !string.IsNullOrEmpty(tel))
            {
                MessageBoxResult result = MessageBox.Show("Добавить новый товар?", "Проверка данных", MessageBoxButton.OKCancel, MessageBoxImage.Question);

                if (result == MessageBoxResult.OK)
                {
                    try
                    {
                        dbContext.Add_Personal(f, i, p, t, tel);
                        Clear();

                        MessageBox.Show("Новый сотрудник добавлен!", "Статус операции", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    catch
                    {
                        MessageBox.Show(" Добавление невозможно \n Ошибка базы данных!", "Ошибка добавления", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }                  
            }
            else
            {
                MessageBox.Show("Добавление невозможно \n Все поля должны быть заполнены!", "Ошибка добавления", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
        private void ClearNewEmployee(object sender, RoutedEventArgs e)
        {
            Clear();
        }

        private void Clear()
        {
            tbFamEmp.Clear();
            tbNameEmp.Clear();
            tbPatEmp.Clear();
            tbTitEmp.Clear();
            tbTelEmp.Clear();
        }
    }
}
