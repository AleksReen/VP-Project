using System;
using System.Windows;
using System.Data.Entity;

namespace VPproject
{
    public partial class wOtchClientMogilev : Window
    {
        private StroitelEntities dbContext;

        public wOtchClientMogilev()
        {
            dbContext = new StroitelEntities();
            InitializeComponent();         
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            GetData();
        }

        private void GetData()
        {
            dbContext.Клиенты_Могилёв.Load();
            dgClientMogilev.DataContext = dbContext.Клиенты_Могилёв.Local.ToBindingList();
            tbSt.Text = "Cформирован";
            tbCount.Text = Convert.ToString(dbContext.Клиенты_Могилёв.Local.Count);
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
