using System;
using System.Windows;

namespace VPproject
{
    public partial class wNewRemont : Window
    {
        public static StroitelEntities dbContext { get; set; }

        public wNewRemont()
        {
            dbContext = new StroitelEntities();
            InitializeComponent();
        }

        private void AddNewRem(object sender, RoutedEventArgs e)
        {
            try
            {
                int cl = Convert.ToInt32(cbCl.SelectedValue);
                int pr = Convert.ToInt32(cbPr.SelectedValue);

                MessageBoxResult result = MessageBox.Show("Оформить новый ремонт?", "Проверка данных", MessageBoxButton.OKCancel, MessageBoxImage.Question);
                if (result == MessageBoxResult.OK)
                {
                    dbContext.ADD_NEW_REMONT(cl, pr);
                    MessageBox.Show("Новый ремонт оформлен!", "Статус операции",MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch
            {
                MessageBox.Show("Добавление невозможно \n Проверьте заполнение полей!!!", "Ошибка добавления", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
