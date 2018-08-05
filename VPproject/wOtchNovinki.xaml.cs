using System;
using System.Windows;
using System.Data.Entity;

namespace VPproject
{
    public partial class wOtchNovinki : Window
    {
        private StroitelEntities dbContext;
        public wOtchNovinki()
        {
            dbContext = new StroitelEntities();
            InitializeComponent();

            GetData();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            GetData();
        }

        private void GetData()
        {
            dbContext.Обзор_новинок.Load();
            dgNovinki.DataContext = dbContext.Обзор_новинок.Local.ToBindingList();

            tbSt.Text = "Cформирован";
            tbCount.Text = Convert.ToString(dbContext.Обзор_новинок.Local.Count);
            tbDate.Text = Convert.ToString(DateTime.Today.ToString("dd MMMM yyyy"));
        }

        private void Print(object sender, RoutedEventArgs e)
        {
            Other.Prints(dgNovinki);
        }
        
        private void Export(object sender, RoutedEventArgs e)
        {
            string name = "Отчет новинки в ассортименте ";
            Other.Exports(dgNovinki, name);
        }   
    }
}
