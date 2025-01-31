using Colina_Employee.API.DataAccess;
using Colina_Employee.API.DTO;
using Colina_Employee.API.Model;
using Microsoft.EntityFrameworkCore;

    namespace Colina_Employee.API.Logic
    {
        public class EmployeeLogicsAndDBCalls
        {
            private readonly EmployeeDbContext dbContext;

            public EmployeeLogicsAndDBCalls(EmployeeDbContext dbContext)
            {
                this.dbContext = dbContext;
            }

            public async Task<IEnumerable<EmployeeDTO>> GetAllEmployeesAsync()
            {
                return await dbContext.Employees
                    .Where(e => !e.IsDeleted)
                    .Select(e => ToEmployeeDTOStatic(e))
                    .ToListAsync();
            }

            public async Task<EmployeeDTO> GetEmployeeByIdAsync(Guid id)
            {
                var employee = await dbContext.Employees.FindAsync(id);
                if (employee == null || employee.IsDeleted)
                {
                    return null;
                }
                return ToEmployeeDTOStatic(employee);
            }

        // Checking of email to avoid duplicates
        public async Task<bool> IsEmailTakenAsync(string email)
        {
            return await dbContext.Employees.AnyAsync(e => e.Email == email);
        }

        // Adding employee
        public async Task<EmployeeDTO> AddEmployeeAsync(EmployeeDTO employeeDto)
        {

            var employee = new Employee
            {
                Id = Guid.NewGuid(),
                Name = employeeDto.Name,
                Email = employeeDto.Email,
                Position = employeeDto.Position,
                Salary = employeeDto.Salary,
                SSSNumber = employeeDto.SSSNumber,
                PagibigNumber = employeeDto.PagibigNumber
            };
            dbContext.Employees.Add(employee);
            await dbContext.SaveChangesAsync();
            return ToEmployeeDTOStatic(employee);
        }

        public async Task<EmployeeDTO> UpdateEmployeeAsync(Guid id, EmployeeDTO employeeDto)
            {
                var employee = await dbContext.Employees.FindAsync(id);
                if (employee == null || employee.IsDeleted)
                {
                    return null;
                }
                employee.Name = employeeDto.Name;
                employee.Email = employeeDto.Email;
                employee.Position = employeeDto.Position;
                employee.Salary = employeeDto.Salary;
                employee.SSSNumber = employeeDto.SSSNumber;
                employee.PagibigNumber = employeeDto.PagibigNumber;
                await dbContext.SaveChangesAsync();
                return ToEmployeeDTOStatic(employee);
            }

            public async Task<bool> DeleteEmployeeAsync(Guid id)
            {
                var employee = await dbContext.Employees.FindAsync(id);
                if (employee == null)
                {
                    return false;
                }
                employee.IsDeleted = true;
                await dbContext.SaveChangesAsync();
                return true;
            }


        // Mapping to EmployeeDTO
        private static EmployeeDTO ToEmployeeDTOStatic(Employee employee)
        {
            return new EmployeeDTO
            {
                Id = employee.Id,
                Name = employee.Name,
                Email = employee.Email,
                Position = employee.Position,
                Salary = employee.Salary,
                SSSNumber = employee.SSSNumber,
                PagibigNumber = employee.PagibigNumber
            };
        }

        // Mapping to UpdatingEmployeeDto
        private static UpdatingEmployeeDto ToUpdatingEmployeeDtoStatic(Employee employee)
        {
            return new UpdatingEmployeeDto
            {
                Name = employee.Name,
                Email = employee.Email,
                Position = employee.Position,
                Salary = employee.Salary,
                SSSNumber = employee.SSSNumber,
                PagibigNumber = employee.PagibigNumber
            };
        }

    }
}


