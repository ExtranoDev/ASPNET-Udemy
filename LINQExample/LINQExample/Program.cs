﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace LINQExample
{
    class Employee
    {
        public int EmpId { get; set; }
        public string EmpName { get; set; }
        public string Job { get; set; }
        public string City { get; set; }
        public double Salary { get; set; }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Employee> employees = new List<Employee>() {
                new Employee() { EmpId = 1011, EmpName="John", Job="Videographer", City="Akure", Salary=2000},
                new Employee() { EmpId = 2231, EmpName="Andrew", Job="Networker", City="Ondo", Salary=5000},
                new Employee() { EmpId = 3452, EmpName="Joshua", Job="Developer", City="Ife", Salary=10000},
                new Employee() { EmpId = 4987, EmpName="David", Job="Developer", City="Lekki", Salary=10000},
                new Employee() { EmpId = 2465, EmpName="Sukanmi", Job="Technician", City="Airport", Salary = 3000},
                new Employee() { EmpId = 6001, EmpName="Emmanuel", Job="Developer", City="Lagos Island", Salary = 10000},
                new Employee() { EmpId = 7240, EmpName="Eng. Enouch", Job="Enginner", City="Ikeja", Salary=8000}
            };
            //IEnumerable<Employee> orderedEmployee =  employees.OrderBy(emp => emp.EmpName);
            //IEnumerable<Employee> orderedEmployee =  employees.OrderByDescending(emp => emp.EmpName);
            IEnumerable<Employee> orderedEmployee =  employees.OrderBy(emp => emp.Job)
                .ThenBy(emp => emp.EmpName);
            Employee justOne = orderedEmployee.FirstOrDefault(x => x.Job == "Developer"); // Or use Last||LastOrDefault||First
            IEnumerable<Employee> result = employees.Where(x => x.Job == "Developer");

            Employee elementAtResult = employees.Where(emp => emp.Job == "Developer").ElementAtOrDefault(0); // Or Use ElementAt()
            Employee singleResult = employees.Where(emp => emp.Job == "Developer").Single(emp => emp.City == "Ife"); // Or Use SingleOrDefault()
            IEnumerable<int> intResult = employees.Select(emp => emp.EmpId);

            foreach (Employee employee in result)
            {
                Console.WriteLine("\nEmployee ID: " + employee.EmpId);
                Console.WriteLine("Employee Name: " + employee.EmpName);
                Console.WriteLine("Job Title: " + employee.Job);
                Console.WriteLine("Lives : " + employee.City);
                Console.WriteLine("Salary : " + employee.Salary);
            }

            foreach (int emp in intResult)
            {
                Console.WriteLine("\nEmployee ID: " + emp);
            }

            Console.WriteLine("\n========================\n");
            if (justOne != null)
            {
                Console.WriteLine("\nEmployee ID: " + justOne.EmpId);
                Console.WriteLine("Employee Name: " + justOne.EmpName);
                Console.WriteLine("Job Title: " + justOne.Job);
                Console.WriteLine("Lives : " + justOne.City);
                Console.WriteLine("Salary : " + justOne.Salary);
            }

            Console.WriteLine("\n========================\n");
            if (elementAtResult != null)
            {
                Console.WriteLine("\nEmployee ID: " + elementAtResult.EmpId);
                Console.WriteLine("Employee Name: " + elementAtResult.EmpName);
                Console.WriteLine("Job Title: " + elementAtResult.Job);
                Console.WriteLine("Lives : " + elementAtResult.City);
                Console.WriteLine("Salary : " + elementAtResult.Salary);
            }

            Console.WriteLine("\n========================\n");
            foreach (Employee employee in orderedEmployee)
            {
                Console.WriteLine("\nEmployee ID: " + employee.EmpId);
                Console.WriteLine("Employee Name: " + employee.EmpName);
                Console.WriteLine("Job Title: " + employee.Job);
                Console.WriteLine("Lives : " + employee.City);
                Console.WriteLine("Salary : " + employee.Salary);
            }


            // LINQ group property
            double minSalary = employees.Min(emp => emp.Salary);
            double maxSalary = employees.Max(emp => emp.Salary);
            double sumSalary = employees.Sum(emp => emp.Salary);
            double avgSalary = employees.Average(emp => emp.Salary);
            int countSalary = employees.Count();


            Console.WriteLine("\n\nData Full Statistical Details:");
            Console.WriteLine("Minimum Salary: " + minSalary);
            Console.WriteLine("Maximum Salary: " + maxSalary);
            Console.WriteLine("Sum of all Salary : " + sumSalary);
            Console.WriteLine("Average Salary : " + avgSalary);
            Console.WriteLine("Toral Salary count: " + countSalary);

            Console.ReadKey();
        }
    }
}
