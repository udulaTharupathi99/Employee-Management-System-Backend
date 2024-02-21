using EmpManagement.Core.Models;
using EmpManagement.Core.Requests;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace EmpManagement.BusinessLogic.Interfaces
{
    public interface IDepartmentService
    {
        Task<List<DepartmentModel>> GetAllDepartments();
        Task<DepartmentModel> GetDepartmentById(int departmentId);
        Task<int> AddDepartment(AddDepartmentRequest department);
        Task<bool> UpdateDepartment(DepartmentModel department);
        Task<bool> DeleteDepartment(int departmentId);

    }
}
