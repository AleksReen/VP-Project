using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Collections.ObjectModel;
using System.Data;

namespace WpfDiplom
{
    /// <summary>
    /// Логика взаимодействия для Providers.xaml
    /// </summary>
    public partial class Providers : Page
    {
         public static StroitelEntities DataEntitiesProviders { get; set; }
        ObservableCollection<Поставщик> ListProviders;
        
        public Providers()
        {
            DataEntitiesProviders = new StroitelEntities();
            InitializeComponent();
            ListProviders = new ObservableCollection<Поставщик>();
        }

        private void Page_LoadedProviders(object sender, RoutedEventArgs e)
        {
            ZagrProv();
            tbSt.Text = "ЗАГРУЖЕНО";
        }

        private void ZagrProv()
        {
            ListProviders.Clear();

            var providers = DataEntitiesProviders.Поставщик;
            var queryProvider = from provider in providers
                                orderby provider.Код_поставщика
                                select provider;
            foreach (Поставщик provider in queryProvider)
            {
                ListProviders.Add(provider);
            }
            dgProviders.ItemsSource = ListProviders;
            tbCount.Text = Convert.ToString(ListProviders.Count());
            tbDate.Text = Convert.ToString(DateTime.Today.ToString("dd MMMM yyyy"));
        }

        #region Метод вызова окна новый поставщик
        private void clNewProviders(object sender, RoutedEventArgs e)
        {

            var newProv = new wNewProviders(this);
            newProv.Closed += newProv_Closed;
            newProv.ShowDialog();

        }
        #endregion

        #region Метод делегат на закрытие окна новый поставщик
        void newProv_Closed(object sender, EventArgs e)
        {
            ZagrProv();
        }
        #endregion

        #region вызов окна отчета Поставщик - товар

        private void OtchPostTov(object sender, RoutedEventArgs e)
        {
            var OtchPostTov = new wOtchPostTov();
            OtchPostTov.Show();
        }
        #endregion

        #region Метод удаления поставщика
        private void clDeleteProviders(object sender, RoutedEventArgs e)
        {
            Поставщик prv = dgProviders.SelectedItem as Поставщик;
            
            if (prv != null)
            {
                MessageBoxResult result = MessageBox.Show("Удалить данные", "Предупреждение", MessageBoxButton.OKCancel);
                if (result == MessageBoxResult.OK)
                {
                    try
                    {
                        DataEntitiesProviders.Поставщик.Remove(prv);
                        DataEntitiesProviders.SaveChanges();

                        dgProviders.SelectedIndex = dgProviders.SelectedIndex == 0 ? 1 : dgProviders.SelectedIndex - 1;
                        ListProviders.Remove(prv);

                        tbCount.Text = Convert.ToString(ListProviders.Count());
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
        private void clSaveProvider(object sender, RoutedEventArgs e)
        {
            DataEntitiesProviders.SaveChanges();
            dgProviders.IsReadOnly = true;
            tbSt.Text = "СОХРАНЕНО";
        }
        #endregion

        #region Метод редактирования
        private void clEditProvider(object sender, RoutedEventArgs e)
        {
            dgProviders.IsReadOnly = false;
            dgProviders.BeginEdit();
            tbSt.Text = "РЕДАКТИРУЕТСЯ";
        }
        #endregion

        #region Метод обновления
        private void clRefreshProvider(object sender, RoutedEventArgs e)
        {
            ZagrProv();
        }
        #endregion

        #region Метод поиска через LINQ
        private void clFindProvider(object sender, RoutedEventArgs e)
        {
            string name = tbOrg.Text;
            DataEntitiesProviders = new StroitelEntities();
            ListProviders.Clear();
            var providers = DataEntitiesProviders.Поставщик;
            var queryProvider = from provaider in providers
                                where provaider.Наименование.Contains(name)
                                select provaider;
            foreach (Поставщик prv in queryProvider)
            {
                ListProviders.Add(prv);
            }
            if (ListProviders.Count > 0)
            {
                dgProviders.ItemsSource = ListProviders;
                tbCount.Text = Convert.ToString(ListProviders.Count());
            }
            else
                MessageBox.Show("Организация поставщика \n" + name + "\n не найдена",
                     "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
        #endregion

        #region Метод выход из программы
        private void clExit(object sender, RoutedEventArgs e)
        {
            Other.Exit();
        }
        #endregion
    }
}
