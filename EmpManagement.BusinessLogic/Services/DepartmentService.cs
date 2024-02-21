using EmpManagement.BusinessLogic.Interfaces;
using EmpManagement.Core.Models;
using EmpManagement.Core.Requests;
using EmpManagement.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpManagement.BusinessLogic.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly string _connectionString;
        

        public DepartmentService(string connectionString)
        {
            _connectionString = connectionString;          
        }

        
        public async Task<List<DepartmentModel>> GetAllDepartments()
        {
            var departments = new List<DepartmentModel>();
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = new SqlCommand("SELECT * FROM Departments", connection);
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    departments.Add(new DepartmentModel
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        Code = Convert.ToString(reader["Code"]),
                        Name = Convert.ToString(reader["Name"]),
                        Location = Convert.ToString(reader["Location"]),
                        Description = Convert.ToString(reader["Description"])
                        
                    });
                }
            }
            return departments;
        }

        public async Task<DepartmentModel> GetDepartmentById(int departmentId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = new SqlCommand("SELECT * FROM Departments WHERE Id = @Id", connection);
                command.Parameters.AddWithValue("@Id", departmentId);
                var reader = command.ExecuteReader();
                if (reader.Read())
                {
                    return new DepartmentModel
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        Code = Convert.ToString(reader["Code"]),
                        Name = Convert.ToString(reader["Name"]),
                        Location = Convert.ToString(reader["Location"]),
                        Description = Convert.ToString(reader["Description"])
                    };
                }
                return null;
            }
        }
        public async Task<int> AddDepartment(AddDepartmentRequest department)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = new SqlCommand("INSERT INTO Departments (Code, Name, Location, Description) VALUES (@Code, @Name, @Location, @Description); SELECT SCOPE_IDENTITY();", connection);
                command.Parameters.AddWithValue("@Code", department.Code);
                command.Parameters.AddWithValue("@Name", department.Name);
                command.Parameters.AddWithValue("@Location", department.Location);
                command.Parameters.AddWithValue("@Description", department.Description);
                return Convert.ToInt32(command.ExecuteScalar());
            }
        }

        public async Task<bool> UpdateDepartment(DepartmentModel department)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = new SqlCommand("UPDATE Departments SET Code = @Code, Name = @Name, Location = @Location, Description = @Description WHERE Id = @Id", connection);
                command.Parameters.AddWithValue("@Code", department.Code);
                command.Parameters.AddWithValue("@Name", department.Name);
                command.Parameters.AddWithValue("@Id", department.Id);
                command.Parameters.AddWithValue("@Location", department.Location);
                command.Parameters.AddWithValue("@Description", department.Description);
                //command.ExecuteNonQuery();
                var res =  Convert.ToInt32(command.ExecuteNonQuery());

                return res > 0;
            }
        }

        public async Task<bool> DeleteDepartment(int departmentId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = new SqlCommand("DELETE FROM Departments WHERE Id = @Id", connection);
                command.Parameters.AddWithValue("@Id", departmentId);
                //command.ExecuteNonQuery();

                var res = Convert.ToInt32(command.ExecuteNonQuery());

                return res > 0;
            }
        }
    }
}
