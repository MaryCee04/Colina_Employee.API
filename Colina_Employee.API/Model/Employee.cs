using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Colina_Employee.API.Model
{
    public class Employee
    {
        public Guid Id {  get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Position { get; set; }
        public decimal Salary { get; set; }
        public string SSSNumber { get; set; }
        public string PagibigNumber { get; set; }
        public bool IsDeleted { get; set; } = false;


    }
}
