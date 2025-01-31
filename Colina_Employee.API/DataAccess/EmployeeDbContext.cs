using Colina_Employee.API.Model;
using Microsoft.EntityFrameworkCore;

namespace Colina_Employee.API.DataAccess
{
    public class EmployeeDbContext : DbContext
    {
        public EmployeeDbContext(DbContextOptions<EmployeeDbContext> options) : base(options)
        {

        }
        public DbSet<Employee> Employees { get; set; }
    }

}

