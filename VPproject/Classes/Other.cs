using System;
using System.Text;
using System.Windows;
using Microsoft.Win32;
using System.Windows.Input;
using System.Windows.Controls;
using System.IO;


namespace VPproject
{
    class Other
    {
        public static void Exit()
        {
            MessageBoxResult result = MessageBox.Show("Вы действительно хотите завершить работу?", "Завершение работы АСУ СтройМир", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                Application.Current.Shutdown();
            }
        }

        public static void Prints(System.Windows.Media.Visual visual)
        {
            PrintDialog p = new PrintDialog();
            if (p.ShowDialog() == true)
            {
                p.PrintVisual(visual, "Отчет");
            }
        }

        public static void Exports(DataGrid DG, string name)
        {

            DG.SelectAllCells();
            DG.ClipboardCopyMode = DataGridClipboardCopyMode.IncludeHeader;
            ApplicationCommands.Copy.Execute(null, DG);


            string rezult = (string)Clipboard.GetData(DataFormats.Text);
            DG.UnselectAllCells();

            SaveFileDialog dgl = new SaveFileDialog();
            string dateNow = DateTime.Now.ToString("(dd MMMM yyyy)");
            dgl.FileName = name + dateNow;
            dgl.DefaultExt = ".xlsx";
            dgl.Filter = "Excel (.xlsx)|*.xls";

            bool? result1 = dgl.ShowDialog();

            if (result1 == true)
            {
                string filename = dgl.FileName;
                StreamWriter file = new StreamWriter(filename, true, Encoding.GetEncoding(1251));
                file.WriteLine(rezult);
                file.Close();
                MessageBox.Show("Отчет успешно экспортирован в Excel", "Экспорт данных");
            }
        }
    }
}
