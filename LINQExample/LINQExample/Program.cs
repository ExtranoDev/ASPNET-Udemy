using System;
using System.Collections.Generic;
using System.Linq;

namespace LINQExample
{
    class Employee
    {
        public int EmpId { get; set; }
        public string EmpName { get; set; }
        public string Job { get; set; }
        public string City { get; set; }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Employee> employees = new List<Employee>() {
                new Employee() { EmpId = 1011, EmpName="John", Job="Videographer", City="Akure"},
                new Employee() { EmpId = 2231, EmpName="Andrew", Job="Networker", City="Ondo"},
                new Employee() { EmpId = 3452, EmpName="Joshua", Job="Developer", City="Ife"},
                new Employee() { EmpId = 4987, EmpName="David", Job="Developer", City="Lekki"},
                new Employee() { EmpId = 2465, EmpName="Sukanmi", Job="Technician", City="Airport"},
                new Employee() { EmpId = 6001, EmpName="Emmanuel", Job="Developer", City="Lagos Island"},
                new Employee() { EmpId = 7240, EmpName="Eng. Enouch", Job="Enginner", City="Ikeja"}
            };
            IEnumerable<Employee> result = employees.Where(x => x.Job == "Developer");

            foreach (Employee employee in result)
            {
                Console.WriteLine("\nEmployee ID: " + employee.EmpId);
                Console.WriteLine("Employee Name: " + employee.EmpName);
                Console.WriteLine("Job Title: " + employee.Job);
                Console.WriteLine("Lives : " + employee.City);
            }

            Console.ReadKey();
        }
    }
}
