
using System.Windows;


namespace WpfDiplom
{
    /// <summary>
    /// Логика взаимодействия для wNewProviders.xaml
    /// </summary>
    public partial class wNewProviders : Window
    {
        public static StroitelEntities NewPr { get; set; }

        public wNewProviders(Providers newPr)
        {
            NewPr = new StroitelEntities();
            InitializeComponent();

        }
        private void AddNewPr(object sender, RoutedEventArgs e)
        {
            string name = tbNameProv.Text;
            string adress = tbAdressProv.Text;
            string tel = tbTelProv.Text;
            string cont = tbContact.Text;

            if (! string.IsNullOrWhiteSpace(name) & ! string.IsNullOrWhiteSpace(adress) )
            {
                try
                {
                    MessageBoxResult result =
                            MessageBox.Show("Добавить нового поставщика?", "Проверка данных", MessageBoxButton.OKCancel);
                    if (result == MessageBoxResult.OK)
                    {
                        NewPr.Add_Provider(name, adress, tel, cont);
                        MessageBox.Show("Новый поставщик добавлен!", "Статус операции");

                        TbClear();
                    }
                }
                catch
                {
                    MessageBox.Show(" Добавление невозможно \n Проверьте заполнение полей!!!", "Ошибка добавления");
                }
            }
            else { MessageBox.Show("Заполните наименование и адресс поставщика", "Ошибка добавления"); }
        }
        private void ClearNewPr(object sender, RoutedEventArgs e)
        {
            TbClear();
        }
        private void TbClear()
        {
            tbNameProv.Clear();
            tbAdressProv.Clear();
            tbTelProv.Clear();
            tbContact.Clear();
        }
    }
}
