using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using DemoWpfPosgreSQL.DataAccess;
using DemoWpfPosgreSQL.Model;
using Npgsql;

namespace DemoWpfPosgreSQL.ViewModel
{
    public class EmployeeViewModel : BaseViewModel
    {
        #region Properties
        private BaseRepository<Employee> employe_repository_ = new EmployeeRepository("employees", Npgsql.NpgsqlFactory.Instance);
        private ObservableCollection<Employee> employees_list_;
        public ObservableCollection<Employee> Employees
        {
            get
            {
                return employees_list_;
            }
            set
            {
                employees_list_ = value;
            }
        }
        private Employee selected_employee_;
        public Employee SelectedEmployee {
            get {
                return selected_employee_;
            }
            set
            {
                selected_employee_ = value;
                if (selected_employee_ != null)
                {
                    Email = selected_employee_.Email;
                    EmployeeName = selected_employee_.EmployeeName;
                    Mobile = selected_employee_.Mobile;
                    Age = selected_employee_.Age;
                } else
                {
                    Email = null;
                    EmployeeName = null;
                    Mobile = null;
                    Age = null;
                }
                
            }
        }
        private string email_;
        public string Email
        {
            get => email_;
            set
            {
                if (email_ != value)
                {
                    email_ = value;
                    OnPropertyChanged("Email");
                }
            }
        }
        private string mobile_;
        public string Mobile
        {
            get => mobile_;
            set
            {
                if (mobile_ != value)
                {
                    mobile_ = value;
                    OnPropertyChanged("Mobile");
                }
            }
        }
        private string employee_name_;
        public string EmployeeName
        {
            get => employee_name_;
            set
            {
                if (employee_name_ != value)
                {
                    employee_name_ = value;
                    OnPropertyChanged("EmployeeName");
                }
            }
        }
        private int? age_;
        public int? Age
        {
            get => age_;
            set
            {
                if (age_ != value)
                {
                    age_ = value;
                    OnPropertyChanged("Age");
                }
            }
        }
        #endregion
        public EmployeeViewModel()
        {
            employees_list_ = new ObservableCollection<Employee>(employe_repository_.GetAll());
            SelectedEmployee = new Employee();
        }
        #region ICommand
        private ICommand updater_;
        public ICommand UpdateCommand
        {
            get
            {
                if (updater_ == null)
                    updater_ = new RelayCommand(param => EditEmployee(), null);
                return updater_;
            }
            set
            {
                updater_ = value;
            }
        }
        private ICommand delete_command_;
        public ICommand DeleteCommand
        {
            get
            {
                if (delete_command_ == null)
                    delete_command_ = new RelayCommand(param => DeleteEmployee(), null);
                return delete_command_;
            }
            set
            {
                delete_command_ = value;
            }
        }
        private ICommand insert_command_;
        public ICommand InsertCommand
        {
            get
            {
                if (insert_command_ == null)
                    insert_command_ = new RelayCommand(param => InsertEmployee(), null);
                return insert_command_;
            }
            set
            {
                insert_command_ = value;
            }
        }
        private void InsertEmployee()
        {
            Employee employee = new Employee();
            employee.EmployeeName = EmployeeName;
            employee.Email = Email;
            employee.Mobile = Mobile;
            employee.Age = Age;
            employe_repository_.Add(employee);
            Employees.Add(employee);

        }
        private void EditEmployee() {
            if (selected_employee_ != null)
            {
                selected_employee_.EmployeeName = EmployeeName;
                selected_employee_.Email = Email;
                selected_employee_.Mobile = Mobile;
                selected_employee_.Age = Age;
                employe_repository_.Update(selected_employee_);
            }
        }
        private void DeleteEmployee()
        {
            employe_repository_.Delete(SelectedEmployee);
            employees_list_.Remove(SelectedEmployee);
        }
        #endregion
    }
}
