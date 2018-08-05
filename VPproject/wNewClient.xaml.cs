using System.Windows;

namespace VPproject
{
    public partial class wNewClient : Window
    {
        private StroitelEntities dbContext { set; get; }

        public wNewClient(Clients newClient)
        {
            dbContext = new StroitelEntities();
            InitializeComponent();
        }

        private void AddNewClient(object sender, RoutedEventArgs e)
        {
            string fam = tbFamCl.Text;
            string name = tbNameCl.Text;
            string pat = tbPatCl.Text;
            string org = tbOrgCl.Text;
            string ad = tbAdCl.Text;
            string tel = tbTelephoneCl.Text;
            string can = tbCanalCl.Text;
          
            MessageBoxResult result = MessageBox.Show("Добавить нового клиента?", "Проверка данных", MessageBoxButton.OKCancel, MessageBoxImage.Question);

            if (result == MessageBoxResult.OK)
            {
                try
                {
                    dbContext.Add_Client(fam, name, pat, org, ad, tel, can);
                    Clear();
                    MessageBox.Show("Новый клиент добавлен!", "Статус операции", MessageBoxButton.OK, MessageBoxImage.Information);                   
                }

                catch
                {
                    MessageBox.Show(" Добавление невозможно \n Проверьте заполнение полей!", "Ошибка добавления", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void ClearNewCl(object sender, RoutedEventArgs e)
        {
            Clear();
            MessageBox.Show("Форма очищена!", "Статус операции", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void Clear()
        {
            tbFamCl.Clear();
            tbNameCl.Clear();
            tbPatCl.Clear();
            tbOrgCl.Clear();
            tbAdCl.Clear();
            tbTelephoneCl.Clear();
            tbCanalCl.SelectedIndex = -1;
        }
    }
}
