using EmpManagement.BusinessLogic.Interfaces;
using EmpManagement.Core.Models;
using EmpManagement.Core.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmpManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;

        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        [HttpGet("/department")]
        public async Task<IActionResult> GetAllDepartments()
        {
            try
            {
                var departments = await _departmentService.GetAllDepartments();
                return Ok(departments);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("/department/{id}")]
        public async Task<IActionResult> GetDepartmentById(int id)
        {
            try
            {
                var department = await _departmentService.GetDepartmentById(id);
                if (department == null)
                {
                    return NotFound("Department not found");
                }

                return Ok(department);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost("/department")]
        public async Task<IActionResult> AddDepartment(AddDepartmentRequest request)
        {
            try
            {
                var departmentId = await _departmentService.AddDepartment(request);
                return Ok(departmentId);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("/department/{id}")]
        public async Task<IActionResult> UpdateDepartment(int id, DepartmentModel department)
        {
            try
            {
                if (id != department.Id)
                {
                    return BadRequest("Department ID mismatch");
                }

                var responce = await _departmentService.UpdateDepartment(department);

                if (responce)
                {
                    return Ok("Department updated successfully");
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

        [HttpDelete("/department/{id}")]
        public async Task<IActionResult> DeleteDepartment(int id)
        {
            try
            {
                var responce = await _departmentService.DeleteDepartment(id);
                if (responce)
                {
                    return Ok("Department deleted successfully");
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
