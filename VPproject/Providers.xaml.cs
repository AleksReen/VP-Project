using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Data;
using System.Collections.Generic;

namespace VPproject
{
    public partial class Providers : Page
    {
        public static StroitelEntities dbContext { get; set; }
        private List<Поставщик> ListProviders { get; set; }
        
        public Providers()
        {
            dbContext = new StroitelEntities();
            InitializeComponent();
            ListProviders = new List<Поставщик>();
        }

        private void Page_LoadedProviders(object sender, RoutedEventArgs e)
        {
            GetData();
            tbDate.Text = Convert.ToString(DateTime.Today.ToString("dd MMMM yyyy"));
            tbSt.Text = "ЗАГРУЖЕНО";
        }

        private void GetData()
        {
            if (ListProviders.Any())
            {
                ListProviders.Clear();
            }

            ListProviders = dbContext.Поставщик.OrderBy(p => p.Наименование).ToList();
            
            dgProviders.ItemsSource = ListProviders;
            tbCount.Text = Convert.ToString(ListProviders.Count());       
        }

        private void clNewProviders(object sender, RoutedEventArgs e)
        {

            var newProv = new wNewProviders(this);
            newProv.Closed += newProv_Closed;
            newProv.ShowDialog();

        }

        void newProv_Closed(object sender, EventArgs e)
        {
            GetData();
        }

        private void OtchPostTov(object sender, RoutedEventArgs e)
        {
            var OtchPostTov = new wOtchPostTov();
            OtchPostTov.Show();
        }

        private void clDeleteProviders(object sender, RoutedEventArgs e)
        {
            Поставщик prv = dgProviders.SelectedItem as Поставщик;
            
            if (prv != null)
            {
                MessageBoxResult result = MessageBox.Show("Удалить данные", "Предупреждение", MessageBoxButton.OKCancel, MessageBoxImage.Question);
                if (result == MessageBoxResult.OK)
                {
                    try
                    {
                        dbContext.Поставщик.Remove(prv);
                        dbContext.SaveChanges();

                        GetData();

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

        private void clSaveProvider(object sender, RoutedEventArgs e)
        {
            dbContext.SaveChanges();
            dgProviders.IsReadOnly = true;
            tbSt.Text = "СОХРАНЕНО";
        }

        private void clEditProvider(object sender, RoutedEventArgs e)
        {
            dgProviders.IsReadOnly = false;
            dgProviders.BeginEdit();
            tbSt.Text = "РЕДАКТИРУЕТСЯ";
        }

        private void clRefreshProvider(object sender, RoutedEventArgs e)
        {
            GetData();
        }

        private void clFindProvider(object sender, RoutedEventArgs e)
        {
            string name = tbOrg.Text;

            if (ListProviders.Any())
            {
                ListProviders.Clear();
            }

            ListProviders = dbContext.Поставщик.Where(p => p.Наименование.Contains(name)).OrderBy(p => p.Наименование).ToList();
         
            if (ListProviders.Count > 0)
            {
                dgProviders.ItemsSource = ListProviders;
                tbCount.Text = Convert.ToString(ListProviders.Count());
            }
            else
            {
                MessageBox.Show("Организация поставщика \n" + name + "\n не найдена", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
            }            
        }

        private void clExit(object sender, RoutedEventArgs e)
        {
            Other.Exit();
        }
    }
}
