using EmpManagement.Core.Models;
using EmpManagement.Core.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpManagement.BusinessLogic.Interfaces
{
    public interface IEmployeeService
    {
        Task<List<EmployeeModel>> GetAllEmployees();
        Task<EmployeeModel> GetEmployeeById(int empId);
        Task<int> AddEmployee(AddEmployeeRequest employee, DepartmentModel department);
        Task<bool> UpdateEmployee(UpdateEmployeeRequest employee, DepartmentModel department);
        Task<bool> DeleteEmployee(int empId);
    }
}
