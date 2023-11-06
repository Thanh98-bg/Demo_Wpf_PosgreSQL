using DemoWpfPosgreSQL.Model;
using DemoWpfPosgreSQL.ViewModel;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoWpfPosgreSQL.DataAccess
{
    public class EmployeeRepository : BaseRepository<Employee>
    {
        public EmployeeRepository(DbProviderFactory provider_factory)
            : base(provider_factory)
        {
            table_name_ = "employees";
        }
        protected override void AddItem(Employee item)
        {
            string command_text = $@"INSERT INTO {table_name_}(""EmployeeName"", ""Email"", ""Mobile"", ""Age"") VALUES (@employee_name, @email, @mobile, @age) RETURNING ""EmployeeCode"";";
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
            string command_text = $@"DELETE FROM {table_name_} WHERE ""EmployeeCode""={item.EmployeeCode};";
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
