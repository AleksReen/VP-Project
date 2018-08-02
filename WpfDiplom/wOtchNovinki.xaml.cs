using System;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Data.Entity;
using System.IO;
using Microsoft.Win32;

namespace WpfDiplom
{
    /// <summary>
    /// Логика взаимодействия для wOtchNovinki.xaml
    /// </summary>
    public partial class wOtchNovinki : Window
    {
        private readonly StroitelEntities OtchNovinki;
        public wOtchNovinki()
        {
            InitializeComponent();
            OtchNovinki = new StroitelEntities();
            Start();
        }

        private void Start()
        {
            OtchNovinki.Обзор_новинок.Load();
            dgNovinki.DataContext = OtchNovinki.Обзор_новинок.Local.ToBindingList();

            tbSt.Text = "Отчет сформирован";
            tbCount.Text = Convert.ToString(OtchNovinki.Обзор_новинок.Local.Count);
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
