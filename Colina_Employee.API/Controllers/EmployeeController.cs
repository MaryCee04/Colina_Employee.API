using Colina_Employee.API.DataAccess;
using Colina_Employee.API.DTO;
using Colina_Employee.API.Logic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Colina_Employee.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeeLogicsAndDBCalls employeeService;

        public EmployeeController(EmployeeLogicsAndDBCalls employeeService)
        {
            this.employeeService = employeeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEmployees()
        {
            var allEmployees = await employeeService.GetAllEmployeesAsync();
            return Ok(allEmployees);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployeeById(Guid id)
        {
            var employee = await employeeService.GetEmployeeByIdAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            return Ok(employee);
        }

        //Adding of Employee
        [HttpPost]

        public async Task<IActionResult> AddEmployee(EmployeeDTO employeeDto)
        {

                if (await employeeService.IsEmailTakenAsync(employeeDto.Email))
                {
                    return BadRequest("An employee with this email already exists.");
                }
                var employee = await employeeService.AddEmployeeAsync(employeeDto);
            return CreatedAtAction(nameof(GetEmployeeById), new { id = employee.Id }, employee);
        }

        //Updating the details of a specific employee
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(Guid id, EmployeeDTO employeeDto)
        {
            var employee = await employeeService.UpdateEmployeeAsync(id, employeeDto);
            if (employee == null)
            {
                return NotFound();
            }
            return NoContent();
        }

        //Deleting the details of a specific employee
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(Guid id)
        {
            var result = await employeeService.DeleteEmployeeAsync(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }

        
    }
}
