using DemoWpfPosgreSQL.DataAccess;
using DemoWpfPosgreSQL.Model;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoWpfPosgreSQL.ViewModel
{
    public interface IEmployeeRepository : IRepository<Employee> { }
    public class SimpleEmployeeRepository : IEmployeeRepository
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
    public class EmployeeRepository : BaseRepository<Model.Employee>
    {
        public EmployeeRepository(string table_name, DbProviderFactory provider_factory)
            : base(table_name, provider_factory)
        {

        }
        protected override void AddItem(Employee item)
        {
            string command_text = @"INSERT INTO " + table_name_ + @" (""EmployeeName"", ""Email"", ""Mobile"", ""Age"") VALUES (@employee_name, @email, @mobile, @age) RETURNING ""EmployeeCode"";";
            using (NpgsqlCommand command = (NpgsqlCommand)command_)
            {
                command.CommandText = command_text;
                command.Parameters.AddWithValue("employee_name", item.EmployeeName);
                command.Parameters.AddWithValue("email", item.Email);
                command.Parameters.AddWithValue("mobile", item.Mobile);
                command.Parameters.AddWithValue("age", item.Age);
                item.EmployeeCode = Int32.Parse(command_.ExecuteScalar().ToString());
            }
        }

        protected override void DeleteItem(Employee item)
        {
            string command_text = @"DELETE FROM "+table_name_+ @" WHERE ""EmployeeCode""="+item.EmployeeCode+";";
            using (NpgsqlCommand command = (NpgsqlCommand)command_)
            {
                command.CommandText = command_text;
                command.ExecuteNonQuery();
            }
        }

        protected override ICollection<Employee> ExecuteReader()
        {
            ICollection<Employee> employees_ = new List<Employee>();

            using (DbDataReader reader = command_.ExecuteReader())
                while (reader.Read())
                {
                    Employee employee = ReadEmployee(reader);
                    employees_.Add(employee);
                }
            return employees_;
        }
        private Employee ReadEmployee(DbDataReader reader)
        {
            int? employee_code = reader["employeecode"] as int?;
            string employee_name = reader["employeename"] as string;
            string email = reader["email"] as string;
            string mobile = reader["mobile"] as string;
            int? age = reader["age"] as int?;

            Employee employee = new Employee
            {
                EmployeeCode = employee_code.Value,
                EmployeeName = employee_name,
                Email = email,
                Mobile = mobile,
                Age = age
            };
            return employee;
        }
        protected override void UpdateItem(Employee item)
        {
            var commandText = $@"UPDATE {table_name_}
                SET ""EmployeeName"" = @employee_name, ""Email"" = @email, ""Mobile"" = @mobile, ""Age"" = @age
                WHERE ""EmployeeCode"" = @employee_code;";

            using (NpgsqlCommand command = (NpgsqlCommand)command_)
            {
                command.CommandText = commandText;
                command.Parameters.AddWithValue("employee_name", item.EmployeeName);
                command.Parameters.AddWithValue("email", item.Email);
                command.Parameters.AddWithValue("mobile", item.Mobile);
                command.Parameters.AddWithValue("age", item.Age);
                command.Parameters.AddWithValue("employee_code", item.EmployeeCode);
                command.ExecuteNonQuery();
            }
        }
    }
}
