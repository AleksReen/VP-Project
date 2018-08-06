using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Data;
using System.Collections.Generic;

namespace VPproject
{
    public partial class Remonts : Page
    {
        public static StroitelEntities dbContext { get; set; }       
        public List<Ремонт> ListRemonts { get; set; }

        public Remonts()
        {
            dbContext = new StroitelEntities();
            InitializeComponent();
            tbDate.Text = Convert.ToString(DateTime.Today.ToString("dd MMMM yyyy"));
            ListRemonts = new List<Ремонт>();
        }

        private void Page_LoadedRemonts(object sender, RoutedEventArgs e)
        {
            GetData();
            tbSt.Text = "ЗАГРУЖЕНО";
        }

        private void GetData()
        {
            if (ListRemonts.Any())
            {
                ListRemonts.Clear();
            }

            ListRemonts = dbContext.Ремонт.OrderByDescending(r => r.Номер_акта).ToList();
            
            dgRemonts.ItemsSource = ListRemonts;
            tbCount.Text = Convert.ToString(ListRemonts.Count());         
        }

        private void clNewRemont(object sender, RoutedEventArgs e)
        {
            var newRem = new wNewRemont();
            newRem.Closed += newRem_Closed;
            newRem.ShowDialog();
        }

        void newRem_Closed(object sender, EventArgs e)
        {
            GetData();
        }
      
        private void clDeleteRemont(object sender, RoutedEventArgs e)
        {
            Ремонт rem = dgRemonts.SelectedItem as Ремонт;

            if (rem != null)
            {
                MessageBoxResult result = MessageBox.Show("Удалить данные?", "Предупреждение", MessageBoxButton.OKCancel, MessageBoxImage.Question);
                if (result == MessageBoxResult.OK)
                {
                    try
                    {
                        dbContext.Ремонт.Remove(rem);
                        dbContext.SaveChanges();

                        GetData();

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

        private void clSaveRemont(object sender, RoutedEventArgs e)
        {
            dbContext.SaveChanges();
            dgRemonts.IsReadOnly = true;
            tbSt.Text = "СОХРАНЕНО";
        }

        private void clEditRemont(object sender, RoutedEventArgs e)
        {
            dgRemonts.IsReadOnly = false;
            dgRemonts.BeginEdit();
            tbSt.Text = "РЕДАКТИРУЕТСЯ";
        }

        private void clRefreshRemont(object sender, RoutedEventArgs e)
        {
            GetData();
        }
      
        private void clFindRemont(object sender, RoutedEventArgs e)
        {         
            int number;

            if (int.TryParse(tbRem.Text.Trim(), out number))
            {
                if (ListRemonts.Any())
                {
                    ListRemonts.Clear();
                }

                ListRemonts = dbContext.Ремонт.Where( r => r.Номер_акта == number).OrderBy(r => r.Номер_акта).ToList();

                if (ListRemonts.Count > 0)
                {
                    dgRemonts.ItemsSource = ListRemonts;
                    tbCount.Text = Convert.ToString(ListRemonts.Count());
                }
                else
                {
                    MessageBox.Show("Ремонт с номером акта \n" + number + "\n не найден", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else
            {
                if (String.IsNullOrEmpty(tbRem.Text))
                {
                    GetData();
                }
                else
                {
                    MessageBox.Show("Недопустимое значение", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }              
            }
        }

        private void clExit(object sender, RoutedEventArgs e)
        {
            Other.Exit();
        }

        private void OtchRemont(object sender, RoutedEventArgs e)

        {
            var newOtchRemont = new wOtchRemont();
            newOtchRemont.ShowDialog();
        }

        private void tbRem_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0))
            {
                e.Handled = true;
            }
        }    
    }
}
