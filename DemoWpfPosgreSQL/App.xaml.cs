using DemoWpfPosgreSQL.ViewModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace DemoWpfPosgreSQL
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            DemoWpfPosgreSQL.MainWindow window = new MainWindow();
            EmployeeViewModel vm = new EmployeeViewModel();
            window.DataContext = vm;
            window.Show();
        }
    }
}
