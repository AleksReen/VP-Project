using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Data;
using System.Collections.Generic;

namespace VPproject
{
    public partial class Employees : Page
    {
        public static StroitelEntities dbContext { get; set; }
        private List<Сотрудник> ListEmployees { get; set; }

        public Employees()
        {
            dbContext = new StroitelEntities();
            InitializeComponent();
            ListEmployees = new List<Сотрудник>();
        }
  
        private void Page_LoadedEmployees(object sender, RoutedEventArgs e)
        {
            GetData();
            tbDate.Text = Convert.ToString(DateTime.Today.ToString("dd MMMM yyyy"));
            tbSt.Text = "ЗАГРУЖЕНО";
        }

        private void GetData()
        {
            if (ListEmployees.Any())
            {
                ListEmployees.Clear();
            }

            ListEmployees = dbContext.Сотрудник.OrderBy(e => e.Фамилия).ToList();

            dgEmployees.ItemsSource = ListEmployees;
            tbCount.Text = Convert.ToString(ListEmployees.Count());
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
                MessageBoxResult result = MessageBox.Show("Удалить данные?", "Предупреждение", MessageBoxButton.OKCancel, MessageBoxImage.Question);

                if (result == MessageBoxResult.OK)
                {
                    try
                    {
                        dbContext.Сотрудник.Remove(emp);
                        dbContext.SaveChanges();

                        GetData();
                        
                        MessageBox.Show("Выбранная запись удалена!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Information);
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
            dbContext.SaveChanges();
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

            if (ListEmployees.Any())
            {
                ListEmployees.Clear();
            }
            
            ListEmployees = dbContext.Сотрудник.Where(em => em.Фамилия.Contains(surname)).ToList();

            if (ListEmployees.Count > 0)
            {
                dgEmployees.ItemsSource = ListEmployees;
                tbCount.Text = Convert.ToString(ListEmployees.Count);
            }
            else
            {
                MessageBox.Show("Сотрудник с фамилией \n" + surname + "\n не найден !", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
            }             
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
