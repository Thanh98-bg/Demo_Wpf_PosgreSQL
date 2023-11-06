using DemoWpfPosgreSQL.Model;
using DemoWpfPosgreSQL.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoWpfPosgreSQL.DataAccess
{
    public class SimpleEmployeeRepository : IRepository<Employee>
    {
        private ICollection<Employee> employee_list_ = new List<Employee>()
        {
                new Employee { EmployeeCode = 1, EmployeeName = "Nguyễn Văn Thành", Email = "thanhnv@gmail.com", Mobile = "0387266432", Age = 25},
                new Employee { EmployeeCode = 2, EmployeeName = "Nguyễn Đức Thắng", Email = "thangnd@gmail.com", Mobile = "0396266001", Age = 20},
                new Employee { EmployeeCode = 3, EmployeeName = "Nguyễn Minh Đức", Email = "ducnm@gmail.com", Mobile = "0987266789", Age = 27},
                new Employee { EmployeeCode = 4, EmployeeName = "Nguyễn Mạnh Linh", Email = "linhnm@gmail.com", Mobile = "0302287403", Age = 25},
        };
        public void Add(Employee item)
        {
            employee_list_.Add(item);
        }

        public void Delete(Employee item)
        {
            employee_list_.Remove(item);
        }

        public ICollection<Employee> GetAll()
        {
            return employee_list_;
        }

        public void Update(Employee new_item)
        {
            Employee employee = employee_list_.Where(s => s.EmployeeCode == new_item.EmployeeCode).FirstOrDefault();
            if (employee != null)
            {
                employee.EmployeeName = new_item.EmployeeName;
                employee.Email = new_item.Email;
                employee.Mobile = new_item.Mobile;
                employee.Age = new_item.Age;
            }
        }
    }
}
