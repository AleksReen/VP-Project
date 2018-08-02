using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfDiplom
{
    /// <summary>
    /// Логика взаимодействия для SecurityPage.xaml
    /// </summary>
    public partial class SecurityPage : Page
    {
        public SecurityPage()
        {
            InitializeComponent();
        }

        private void EnterSecurity_Click(object sender, RoutedEventArgs e)
        {
            StroitelEntities userDB = new StroitelEntities();

            string userlogin = String.Empty;
            string userpass = String.Empty;

            try
            {
                userlogin = (from u in userDB.user
                                 where u.login == textBox.Text
                                 select u.login).FirstOrDefault();
                userpass = (from u in userDB.user
                                where u.password == textBox1.Password
                                select u.password).FirstOrDefault();
            }
            catch (Exception)
            {

                MessageBox.Show("Ошибка получения данных", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Error);
            }
               
                try
                {
                    if (textBox.Text == userlogin)
                    {
                        try
                        {
                            if (textBox1.Password == userpass)
                            {

                                NavigationService.Navigate(new Uri("/SelectedPage.xaml", UriKind.Relative));
                            }
                        }
                        catch (SystemException)
                        {
                            MessageBox.Show("Ошибка ввода пароля!!!");
                        }

                    }
                    
                }
                catch (SystemException)
                {
                    MessageBox.Show("Ошибка ввода логина!!!");
                }
        }
    }
}


