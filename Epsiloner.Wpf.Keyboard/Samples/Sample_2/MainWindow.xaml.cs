using System;
using System.Diagnostics;
using System.Windows;
using Epsiloner.Wpf.Keyboard.KeyBinding;
using Microsoft.Win32;

namespace Sample_2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnLoadFromFile_OnClick(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog {InitialDirectory = AppDomain.CurrentDomain.BaseDirectory};
            if (dlg.ShowDialog() == true)
            {
                var filename = dlg.FileName;
                var configs = Configs.Load(filename);
                Manager.Default.LoadFrom(configs, ManagerUpdateMode.Full);
            }
        }

        private void btnSaveToFile_OnClick(object sender, RoutedEventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog {InitialDirectory = AppDomain.CurrentDomain.BaseDirectory};
            if (dlg.ShowDialog() == true)
            {
                var c = Manager.Default.ToEdit();
                c.Save(dlg.FileName);
                Process.Start("notepad.exe", dlg.FileName);
            }
        }
    }
}
