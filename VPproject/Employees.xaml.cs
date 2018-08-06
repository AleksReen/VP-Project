using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Collections.ObjectModel;
using System.Data;


namespace VPproject
{
    public partial class Employees : Page
    {
        public static StroitelEntities DataEntitiesEmployees { get; set; }
        private ObservableCollection<Сотрудник> ListEmployees;

        public Employees()
        {
            DataEntitiesEmployees = new StroitelEntities();
            InitializeComponent();
            ListEmployees = new ObservableCollection<Сотрудник>();
        }
  
        private void Page_LoadedEmployees(object sender, RoutedEventArgs e)
        {
            GetData();
            tbSt.Text = "ЗАГРУЖЕНО";
        }

        private void GetData()
        {
            ListEmployees.Clear();
            var employees = DataEntitiesEmployees.Сотрудник;
            var queryEmployee = from employee in employees
                                orderby employee.Код_сотрудника
                                select employee;
            foreach (Сотрудник employee in queryEmployee)
            {
                ListEmployees.Add(employee);
            }
            dgEmployees.ItemsSource = ListEmployees;
            tbCount.Text = Convert.ToString(ListEmployees.Count());
            tbDate.Text = Convert.ToString(DateTime.Today.ToString("dd MMMM yyyy"));
        }

        private void clNewEmployee(object sender, RoutedEventArgs e)
        {
            var newEmp = new wNewEmployee(this);
            newEmp.Closed += newEmp_Closed;
            newEmp.ShowDialog();      
        }

        void newEmp_Closed(object sender, EventArgs e)
        {
            GetData();
        }

        private void clDeleteEmployee(object sender, RoutedEventArgs e)
        {
            Сотрудник emp = dgEmployees.SelectedItem as Сотрудник;
            if (emp != null)
            {
                MessageBoxResult result = MessageBox.Show("Удалить данные", "Предупреждение", MessageBoxButton.OKCancel, MessageBoxImage.Question);
                if (result == MessageBoxResult.OK)
                {
                    try
                    {
                        DataEntitiesEmployees.Сотрудник.Remove(emp);
                        DataEntitiesEmployees.SaveChanges();

                        dgEmployees.SelectedIndex = dgEmployees.SelectedIndex == 0 ? 1 : dgEmployees.SelectedIndex - 1;
                        ListEmployees.Remove(emp);
                        tbCount.Text = Convert.ToString(ListEmployees.Count());
                        MessageBox.Show("Выбранная вами запись удалена!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Удаление не возможно, есть связанные данные с этой записью", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Выберите строку для удаления", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void clSaveEmployee(object sender, RoutedEventArgs e)
        {
            DataEntitiesEmployees.SaveChanges();
            dgEmployees.IsReadOnly = true;
            tbSt.Text = "СОХРАНЕНО";
        }

        private void clEditEmployee(object sender, RoutedEventArgs e)
        {
            dgEmployees.IsReadOnly = false;
            dgEmployees.BeginEdit();
            tbSt.Text = "РЕДАКТИРУЕТСЯ";
        }

        private void clRefreshEmployee(object sender, RoutedEventArgs e)
        {
            GetData();
        }

        private void clFindEmployee(object sender, RoutedEventArgs e)
        {
            string surname = tbFamily.Text;
            DataEntitiesEmployees = new StroitelEntities();
            ListEmployees.Clear();
            var employees = DataEntitiesEmployees.Сотрудник;
            var queryEmployee = from employee in employees
                                where employee.Фамилия == surname
                                select employee;
            foreach (Сотрудник emp in queryEmployee)
            {
                ListEmployees.Add(emp);
            }
            if (ListEmployees.Count > 0)
            {
                dgEmployees.ItemsSource = ListEmployees;
                tbCount.Text = Convert.ToString(ListEmployees.Count());
            }
            else
                MessageBox.Show("Сотрудник с фамилией \n" + surname + "\n не найден !", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        private void clExit(object sender, RoutedEventArgs e)
        {
            Other.Exit();
        }

        private void OtchVseSt(object sender, RoutedEventArgs e)
        {
            var newOtchVseSt = new wOtchVseSt();
            newOtchVseSt.Show();
        }
    }
}
