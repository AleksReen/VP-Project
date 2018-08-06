using System.Windows;
using VPproject.Models;

namespace VPproject
{
    public partial class wNewProviders : Window
    {
        public static StroitelEntities dbContext { get; set; }

        public wNewProviders(Providers newPr)
        {
            dbContext = new StroitelEntities();
            InitializeComponent();
            this.DataContext = new ProviderValidationModel();
        }
        private void AddNewPr(object sender, RoutedEventArgs e)
        {
            string name = tbNameProv.Text;
            string adress = tbAdressProv.Text;
            string tel = tbTelProv.Text;
            string cont = tbContact.Text;

            if (!string.IsNullOrWhiteSpace(name) && !string.IsNullOrWhiteSpace(adress) )
            {              
                MessageBoxResult result = MessageBox.Show("Добавить нового поставщика?", "Проверка данных", MessageBoxButton.OKCancel, MessageBoxImage.Question);

                if (result == MessageBoxResult.OK)
                {
                    try
                    {
                        dbContext.Add_Provider(name, adress, tel, cont);                      
                        MessageBox.Show("Новый поставщик добавлен!", "Статус операции", MessageBoxButton.OK, MessageBoxImage.Information);

                        Clear();
                    }
                    catch
                    {
                        MessageBox.Show(" Добавление невозможно \n Проверьте заполнение полей!", "Ошибка добавления", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }            
            }
            else
            {
                MessageBox.Show("Заполните наименование и адрес поставщика", "Ошибка добавления", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ClearNewPr(object sender, RoutedEventArgs e)
        {
            Clear();
        }
        private void Clear()
        {
            tbNameProv.Clear();
            tbAdressProv.Clear();
            tbTelProv.Clear();
            tbContact.Clear();
        }
    }
}
