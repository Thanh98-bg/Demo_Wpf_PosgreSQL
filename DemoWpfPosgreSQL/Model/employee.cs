using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections.Specialized;

namespace DemoWpfPosgreSQL.Model
{
    public class Employee : INotifyPropertyChanged
    {
        private int employee_code_;
        private string employee_name_;
        private string email_;
        private string mobile_;
        private int? age_;
        public int EmployeeCode {
            get {
                return employee_code_;
            }
            set {
                employee_code_ = value;
                OnPropertyChanged("EmployeeCode");
            }
        }
        public string EmployeeName
        {
            get
            {
                return employee_name_;
            }
            set
            {
                employee_name_ = value;
                OnPropertyChanged("EmployeeName");
            }
        }
        public string Email
        {
            get
            {
                return email_;
            }
            set
            {
                email_ = value;
                OnPropertyChanged("Email");
            }
        }
        public string Mobile
        {
            get
            {
                return mobile_;
            }
            set
            {
                mobile_ = value;
                OnPropertyChanged("Mobile");
            }
        }
        public int? Age
        {
            get
            {
                return age_;
            }
            set
            {
                age_ = value;
                OnPropertyChanged("Age");
            }
        }
        #region INotifyPropertyChanged Members
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string property_name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property_name));
            }
        }
        #endregion

    }
}
