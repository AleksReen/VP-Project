using System.Windows;

namespace VPproject
{
    public partial class wNewClient : Window
    {
        public static StroitelEntities NewCl { set; get; }

        public wNewClient(Clients newClient)
        {
            NewCl = new StroitelEntities();
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

            try
            {
                MessageBoxResult result =
                        MessageBox.Show("Добавить нового клиента?", "Проверка данных", MessageBoxButton.OKCancel);
                if (result == MessageBoxResult.OK)
                {
                    NewCl.Add_Client(fam, name, pat, org, ad, tel, can);
                    MessageBox.Show("Новый клиент добавлен!", "Статус операции");

                    tbFamCl.Clear();
                    tbNameCl.Clear();
                    tbPatCl.Clear();
                    tbOrgCl.Clear();
                    tbAdCl.Clear();
                    tbTelephoneCl.Clear();
                    tbCanalCl.Clear();
                }
            }
            catch
            {
                MessageBox.Show(" Добавление невозможно \n Проверьте заполнение полей!!!","Ошибка добавления");
            }    

        }

        private void ClearNewCl(object sender, RoutedEventArgs e)
        {
            tbFamCl.Clear();
            tbNameCl.Clear();
            tbPatCl.Clear();
            tbOrgCl.Clear();
            tbAdCl.Clear();
            tbTelephoneCl.Clear();
            tbCanalCl.Clear();

            MessageBox.Show("Форма очищена!", "Статус операции");
        }

    }
}
