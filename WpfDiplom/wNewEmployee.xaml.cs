
using System.Windows;

namespace WpfDiplom
{
    /// <summary>
    /// Логика взаимодействия для wNewEmployee.xaml
    /// </summary>
    public partial class wNewEmployee : Window
    {
        public static StroitelEntities NewE { get; set; }

        public wNewEmployee(Employees newEmp)
        {
            NewE = new StroitelEntities();
            InitializeComponent();
            
        }
        private void AddNewEmployee(object sender, RoutedEventArgs e)
        {
            string f = tbFamEmp.Text;
            string i = tbNameEmp.Text;
            string p = tbPatEmp.Text;
            string t = tbTitEmp.Text;
            string tel = tbTelEmp.Text;

            if (!string.IsNullOrEmpty(f)
                & !string.IsNullOrEmpty(i)
                & !string.IsNullOrEmpty(p)
                & !string.IsNullOrEmpty(t)
                & !string.IsNullOrEmpty(tel))
            {

                try
                {
                    NewE.Add_Personal(f, i, p, t, tel);
                    MessageBox.Show("Новый сотрудник добавлен, Статус операции");
                }
                catch
                {
                    MessageBox.Show(" Добавление невозможно \n Проверьте заполнение полей!!!", "Ошибка добавления");
                }

            }
            else
            {
                MessageBox.Show("Все поля должны быть заполнены!", "Ошибка добавления");
            }

        }
        private void ClearNewEmployee(object sender, RoutedEventArgs e)
        {
            tbFamEmp.Clear();
            tbNameEmp.Clear();
            tbPatEmp.Clear();
            tbTitEmp.Clear();
            tbTelEmp.Clear();
        }
    }
}
