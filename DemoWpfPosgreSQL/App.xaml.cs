using DemoWpfPosgreSQL.DataAccess;
using DemoWpfPosgreSQL.Model;
using DemoWpfPosgreSQL.ViewModel;
using Npgsql;
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
            IRepository<Employee> employee_repository = new EmployeeRepository(NpgsqlFactory.Instance);
            EmployeeViewModel vm = new EmployeeViewModel(employee_repository);
            window.DataContext = vm;
            window.Show();
        }
    }
}
