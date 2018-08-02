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
    /// Логика взаимодействия для wOtchRemont.xaml
    /// </summary>
    public partial class wOtchRemont : Window
    {
        private readonly StroitelEntities OtchRem;
        public wOtchRemont()
        {
            InitializeComponent();
            OtchRem = new StroitelEntities();
            Start();
        }

        private void Start()
        {
            OtchRem.Отчет_по_ремонту.Load();
            dgOtchRem.DataContext = OtchRem.Отчет_по_ремонту.Local.ToBindingList();

            tbSt.Text = "Отчет сформирован";
            tbCount.Text = Convert.ToString(OtchRem.Отчет_по_ремонту.Local.Count);
            tbDate.Text = Convert.ToString(DateTime.Today.ToString("dd MMMM yyyy"));
        }

        private void Print(object sender, RoutedEventArgs e)
        {
            Other.Prints(dgOtchRem);
        }

        
        private void Export(object sender, RoutedEventArgs e)
        {
            string name = "Отчет по ремонту ";
            Other.Exports(dgOtchRem, name);
        }
    }
}
