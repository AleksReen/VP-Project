using System;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Data.Entity;
using System.IO;
using Microsoft.Win32;

namespace VPproject
{
    /// <summary>
    /// Логика взаимодействия для wOtchClientMogilev.xaml
    /// </summary>
    public partial class wOtchClientMogilev : Window
    {
        private readonly StroitelEntities ClientMogilev;
        public wOtchClientMogilev()
        {
            InitializeComponent();

            ClientMogilev = new StroitelEntities();
            ClientMogilev.Клиенты_Могилёв.Load();
            dgClientMogilev.DataContext = ClientMogilev.Клиенты_Могилёв.Local.ToBindingList();
            tbSt.Text = "Cформирован";
            tbCount.Text = Convert.ToString(ClientMogilev.Клиенты_Могилёв.Local.Count);
            tbDate.Text = Convert.ToString(DateTime.Today.ToString("dd MMMM yyyy"));
        }

        private void Print(object sender, RoutedEventArgs e)
        {
            Other.Prints(dgClientMogilev);
        }
        
        private void Export(object sender, RoutedEventArgs e)
        {
            string name = "Отчет клиенты г.Могилёв ";
            Other.Exports(dgClientMogilev, name);
        }
    }
}
