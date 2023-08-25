using System;

namespace Company
{
    /// <summary>
    /// Represents an employee of an organization
    /// </summary>
    public class Employee
    {
        public string EmployeeName { get; set; }
        public int EmployeeID { get; set; }
        public string Email { get; set; }
        public Department dept { get; set; }
    }
}
