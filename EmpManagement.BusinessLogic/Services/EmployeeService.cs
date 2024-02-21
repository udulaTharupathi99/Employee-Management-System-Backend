using EmpManagement.BusinessLogic.Interfaces;
using EmpManagement.Core.Models;
using EmpManagement.Core.Requests;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpManagement.BusinessLogic.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly string _connectionString;

        public EmployeeService(string connectionString)
        {
            _connectionString = connectionString;   
        }

        public async Task<List<EmployeeModel>> GetAllEmployees()
        {
            var employees = new List<EmployeeModel>();
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = new SqlCommand("SELECT * FROM Employees", connection);
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    employees.Add(new EmployeeModel
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        FirstName = Convert.ToString(reader["FirstName"]),
                        LastName = Convert.ToString(reader["LastName"]),
                        Email = Convert.ToString(reader["Email"]),
                        DOB = Convert.ToDateTime(reader["DOB"]),
                        Salary = Convert.ToDouble(reader["Salary"]),
                        DepartmentName = Convert.ToString(reader["DepartmentName"]),
                    });
                }
            }
            return employees;
        }

        public async Task<EmployeeModel> GetEmployeeById(int empId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = new SqlCommand("SELECT * FROM Employees WHERE Id = @Id", connection);
                command.Parameters.AddWithValue("@Id", empId);
                var reader = command.ExecuteReader();
                if (reader.Read())
                {
                    return new EmployeeModel
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        FirstName = Convert.ToString(reader["FirstName"]),
                        LastName = Convert.ToString(reader["LastName"]),
                        Email = Convert.ToString(reader["Email"]),
                        DOB = Convert.ToDateTime(reader["DOB"]),
                        Salary = Convert.ToDouble(reader["Salary"]),
                        DepartmentName = Convert.ToString(reader["DepartmentName"]),
                    };
                }
                return null;
            }
        }

        public async Task<int> AddEmployee(AddEmployeeRequest employee, DepartmentModel department)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                

                //Validate the requesting department
                if (department == null) {  return 0; }

                connection.Open();
                var command = new SqlCommand("INSERT INTO Employees (FirstName, LastName, Email, DOB, Age, Salary, DepartmentName) VALUES (@FirstName, @LastName, @Email, @DOB, @Age, @Salary, @DepartmentName); SELECT SCOPE_IDENTITY();", connection);
                command.Parameters.AddWithValue("@FirstName", employee.FirstName);
                command.Parameters.AddWithValue("@LastName", employee.LastName);
                command.Parameters.AddWithValue("@Email", employee.Email);
                command.Parameters.AddWithValue("@DOB", employee.DOB);
                command.Parameters.AddWithValue("@Age", employee.Age);
                command.Parameters.AddWithValue("@Salary", employee.Salary);
                command.Parameters.AddWithValue("@DepartmentName", department.Name);
                return Convert.ToInt32(command.ExecuteScalar());
            }
        }

        //Validate the requesting department
        private async Task<bool> ValidateDepartmentId(int departmentId, List<DepartmentModel> departmentList)
        {
            var existingDepartment = departmentList.Where(x => x.Id == departmentId).ToList();
            if (existingDepartment.Count == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
            

        }


        public async Task<bool> UpdateEmployee(UpdateEmployeeRequest employee, DepartmentModel department)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                //Validate the requesting department
                if (department == null) { return false; }

                connection.Open();
                var command = new SqlCommand("UPDATE Employees SET FirstName = @FirstName, LastName = @LastName, Email = @Email, DOB = @DOB, Age = @Age, Salary = @Salary, DepartmentName = @DepartmentName WHERE Id = @Id", connection);
                command.Parameters.AddWithValue("@FirstName", employee.FirstName);
                command.Parameters.AddWithValue("@LastName", employee.LastName);
                command.Parameters.AddWithValue("@Email", employee.Email);
                command.Parameters.AddWithValue("@DOB", employee.DOB);
                command.Parameters.AddWithValue("@Age", employee.Age);
                command.Parameters.AddWithValue("@Salary", employee.Salary);
                command.Parameters.AddWithValue("@DepartmentName", department.Name);
                command.Parameters.AddWithValue("@Id", employee.Id);
                var res = Convert.ToInt32(command.ExecuteNonQuery());

                return res > 0;
            }
        }

        public async Task<bool> DeleteEmployee(int empId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = new SqlCommand("DELETE FROM Employees WHERE Id = @Id", connection);
                command.Parameters.AddWithValue("@Id", empId);
                var res = Convert.ToInt32(command.ExecuteNonQuery());

                return res > 0;
            }
        }


    }
}
