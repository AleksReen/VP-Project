using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Collections.ObjectModel;
using System.Data;

namespace VPproject
{
    /// <summary>
    /// Логика взаимодействия для Remont.xaml
    /// </summary>
    public partial class Remonts : Page
    {
        public static StroitelEntities DataEntitiesRemonts { get; set; }
        ObservableCollection<Ремонт> ListRemonts;

        public Remonts()
        {
            DataEntitiesRemonts = new StroitelEntities();
            InitializeComponent();
            ListRemonts = new ObservableCollection<Ремонт>();
        }

        private void Page_LoadedRemonts(object sender, RoutedEventArgs e)
        {
            ZagrRemont();
            tbSt.Text = "ЗАГРУЖЕНО";
        }

        private void ZagrRemont()
        {
            ListRemonts.Clear();
            var remonts = DataEntitiesRemonts.Ремонт;
            var queryRemont = from remont in remonts
                              orderby remont.Номер_акта
                              select remont;
            foreach (Ремонт remont in queryRemont)
            {
                ListRemonts.Add(remont);
            }
            dgRemonts.ItemsSource = ListRemonts;
            tbCount.Text = Convert.ToString(ListRemonts.Count());
            tbDate.Text = Convert.ToString(DateTime.Today.ToString("dd MMMM yyyy"));
        }

        #region Метод вызова окна новый ремонт
        private void clNewRemont(object sender, RoutedEventArgs e)
        {

            var newRem = new wNewRemont();
            newRem.Closed += newRem_Closed;
            newRem.ShowDialog();

        }
        #endregion

        #region Метод делегат на закрытие окна новый ремонт
        void newRem_Closed(object sender, EventArgs e)
        {

            ZagrRemont();

        }
        #endregion

        #region вызов окна отчета
        //private void clwEmployeeRezultDay(object sender, RoutedEventArgs e)
        //{

        //    var newEmpRezDay = new wEmployeeRezultDay(this);
        //    newEmpRezDay.Show();

        //}
        #endregion

        #region Метод удаления ремонта
        private void clDeleteRemont(object sender, RoutedEventArgs e)
        {
            Ремонт rem = dgRemonts.SelectedItem as Ремонт;
            if (rem != null)
            {
                MessageBoxResult result = MessageBox.Show("Удалить данные", "Предупреждение", MessageBoxButton.OKCancel);
                if (result == MessageBoxResult.OK)
                {
                    try
                    {
                        DataEntitiesRemonts.Ремонт.Remove(rem);
                        DataEntitiesRemonts.SaveChanges();

                        dgRemonts.SelectedIndex = dgRemonts.SelectedIndex == 0 ? 1 : dgRemonts.SelectedIndex - 1;
                        ListRemonts.Remove(rem);

                        tbCount.Text = Convert.ToString(ListRemonts.Count());
                        MessageBox.Show("Выбранная вами запись удалена!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Удаление не возможно", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    
                }
            }
            else
            {
                MessageBox.Show("Выберите строку для удаления", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        #endregion

        #region Метод сохранения 
        private void clSaveRemont(object sender, RoutedEventArgs e)
        {
            DataEntitiesRemonts.SaveChanges();
            dgRemonts.IsReadOnly = true;
            tbSt.Text = "СОХРАНЕНО";
        }
        #endregion

        #region Метод редактирования
        private void clEditRemont(object sender, RoutedEventArgs e)
        {
            dgRemonts.IsReadOnly = false;
            dgRemonts.BeginEdit();
            tbSt.Text = "РЕДАКТИРУЕТСЯ";
        }
        #endregion

        #region Метод обновления
        private void clRefreshRemont(object sender, RoutedEventArgs e)
        {
            ZagrRemont();
        }
        #endregion

        #region Метод поиска через LINQ
        private void clFindRemont(object sender, RoutedEventArgs e)
        {
            int number = Convert.ToInt32(tbRem.Text);
            DataEntitiesRemonts = new StroitelEntities();
            ListRemonts.Clear();
            var remonts = DataEntitiesRemonts.Ремонт;
            var queryRemont = from remont in remonts
                                where remont.Номер_акта == number
                                select remont;
            foreach (Ремонт emp in queryRemont)
            {
                ListRemonts.Add(emp);
            }
            if (ListRemonts.Count > 0)
            {
                dgRemonts.ItemsSource = ListRemonts;
                tbCount.Text = Convert.ToString(ListRemonts.Count());
            }
            else
                MessageBox.Show("Ремонт с номером акта \n" + number + "\n не найден",
                     "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
        #endregion

        #region Метод выход из программы
        private void clExit(object sender, RoutedEventArgs e)
        {
            Other.Exit();
        }
        #endregion

        #region Отчет по ремонту

        private void OtchRemont(object sender, RoutedEventArgs e)

        {
            var newOtchRemont = new wOtchRemont();
            newOtchRemont.ShowDialog();
        }

        #endregion
    }
}
