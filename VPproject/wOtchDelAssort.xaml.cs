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
    /// Логика взаимодействия для wOtchDelAssort.xaml
    /// </summary>
    public partial class wOtchDelAssort : Window
    {
        private readonly StroitelEntities DelAssort;
        public wOtchDelAssort()
        {
            InitializeComponent();
            DelAssort = new StroitelEntities();
            tbSt.Text = "Отчет сформирован";
            tbDate.Text = Convert.ToString(DateTime.Today.ToString("dd MMMM yyyy"));
            Start();
        }

        private void Start()
        {
            DelAssort.Выведенный_ассортимент.Load();
            DG.DataContext = DelAssort.Выведенный_ассортимент.Local.ToBindingList();


            tbCount.Text = Convert.ToString(DelAssort.Выведенный_ассортимент.Local.Count);
        }
             private void Print(object sender, RoutedEventArgs e)
        {
            Other.Prints(DG);

        }
        
       private void Export(object sender, RoutedEventArgs e)
        {
            string name = "Отчет выведенный ассортимент ";
            Other.Exports(DG, name);
        }

    }
}
