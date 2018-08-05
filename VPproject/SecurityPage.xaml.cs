using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace VPproject
{
    public partial class SecurityPage : Page
    {
        public SecurityPage()
        {
            InitializeComponent();
        }

        private void EnterSecurity_Click(object sender, RoutedEventArgs e)
        {
            StroitelEntities userDB = new StroitelEntities();
            
            try
            {
                if (!String.IsNullOrEmpty(textBox.Text) || !String.IsNullOrEmpty(textBox1.Password))
                {
                    var userApp = userDB.user.Where(u => u.login == textBox.Text && u.password == textBox1.Password).FirstOrDefault();

                    if (userApp != null)
                    {
                        NavigationService.Navigate(new Uri("/SelectedPage.xaml", UriKind.Relative));
                    }
                    else
                    {
                        MessageBox.Show("Пользователь не существует, проверьте логин и пароль", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("Заполните все поля данные", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка обработки данных", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}


