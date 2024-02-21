using EmpManagement.BusinessLogic.Interfaces;
using EmpManagement.BusinessLogic.Services;
using EmpManagement.Core.Models;
using EmpManagement.Core.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmpManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        private readonly IDepartmentService _departmentService;

        public EmployeeController(IEmployeeService employeeService, IDepartmentService departmentService)
        {
            _employeeService = employeeService;
            _departmentService = departmentService;
        }

        [HttpGet("/employee")]
        public async Task<IActionResult> GetAllEmployees()
        {
            try
            {
                var employees = await _employeeService.GetAllEmployees();
                return Ok(employees);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("/employee/{id}")]
        public async Task<IActionResult> GetEmployeeById(int id)
        {
            try
            {
                var employee = await _employeeService.GetEmployeeById(id);
                if (employee == null)
                {
                    return NotFound("Employee not found");
                }

                return Ok(employee);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost("/employee")]
        public async Task<IActionResult> AddEmployee(AddEmployeeRequest request)
        {
            try
            {
                var department = await _departmentService.GetDepartmentById(request.DepartmentId);
                var employeeId = await _employeeService.AddEmployee(request, department);

                if(employeeId > 0)
                {
                    return Ok(employeeId);
                }
                else
                {
                    return BadRequest("Something went wrong");
                }
                
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("/employee/{id}")]
        public async Task<IActionResult> UpdateEmployee(int id, UpdateEmployeeRequest employee)
        {
            try
            {
                if (id != employee.Id)
                {
                    return BadRequest("Employee ID mismatch");
                }

                var department = await _departmentService.GetDepartmentById(employee.DepartmentId);
                var responce = await _employeeService.UpdateEmployee(employee, department);

                if (responce)
                {
                    return Ok("Employee updated successfully");
                }
                else
                {
                    return BadRequest("Something went wrong");
                }

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("/employee/{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            try
            {
                var responce = await _employeeService.DeleteEmployee(id);
                if (responce)
                {
                    return Ok("Employee deleted successfully");
                }
                else
                {
                    return BadRequest("Something went wrong");
                }

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
