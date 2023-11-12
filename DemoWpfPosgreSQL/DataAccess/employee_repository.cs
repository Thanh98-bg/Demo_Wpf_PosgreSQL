using DemoWpfPosgreSQL.Model;
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
            string command_text = $@"INSERT INTO {table_name_}(""EmployeeName"", ""Email"", ""Mobile"", ""Age"") VALUES ('{item.EmployeeName}', '{item.Email}', '{item.Mobile}', {item.Age}) RETURNING ""EmployeeCode"";";
            using (command_)
            {
                command_.CommandText = command_text;
                item.EmployeeCode = int.Parse(command_.ExecuteScalar().ToString());
            }
        }

        protected override void DeleteItem(Employee item)
        {
            string command_text = $@"DELETE FROM {table_name_} WHERE ""EmployeeCode""={item.EmployeeCode};";
            using (command_)
            {
                command_.CommandText = command_text;
                command_.ExecuteNonQuery();
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
                SET ""EmployeeName"" = '{item.EmployeeName}', ""Email"" = '{item.Email}', ""Mobile"" = '{item.Mobile}', ""Age"" = {item.Age}
                WHERE ""EmployeeCode"" = {item.EmployeeCode};";
            using (command_)
            {
                command_.CommandText = commandText;
                command_.ExecuteNonQuery();
            }
        }
    }
}
