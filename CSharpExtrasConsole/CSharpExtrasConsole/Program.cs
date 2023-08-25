using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using College;
using Company;
using ECommerce;

namespace CSharpExtrasConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //create object
            ClassLibrary1.Product p = new ClassLibrary1.Product() { ProductCost = 1000, DiscountPercentage = 10 };
            Console.WriteLine(p.GetDiscount()); // Call extension and print result of extension

            // ========== Dictionary tutorial ===================
            Dictionary<int, string> employees = new Dictionary<int, string>() {
                {101, "Joshua" },
                {102, "David" },
                {103, "Allen" }
            };

            // foreach loop for dictionary
            foreach (KeyValuePair<int, string> employee in employees)
            {
                Console.WriteLine(employee.Key + ", " + employee.Value);
            }

            Dictionary<int, string>.KeyCollection keys = employees.Keys;
            foreach (int item in keys)
            {
                Console.WriteLine(item);
            }

            Console.WriteLine("-----------------------------");
            foreach (KeyValuePair<int, string> employee in employees)
            {
                Console.WriteLine(employee.Key + ", " + employee.Value);
            }

            // ========== End Dictionary tutorial ===================


            // ========== Collection of Objects tutorial ===============
            List<Product> products = new List<Product>();

            //loop to read data from user
            string choice;
            do
            {
                Console.Write ("Enter ProductID: ");
                int pid = int.Parse(Console.ReadLine());
                Console.Write("Enter Product Name: ");
                string pname = Console.ReadLine();
                Console.Write("Enter Price: ");
                long price = long.Parse(Console.ReadLine());
                Console.Write("Enter Date of Manufacture (yyyy-MM-dd): ");
                DateTime dom = DateTime.Parse(Console.ReadLine());

                //Create a new object of product class
                Product product = new Product() { 
                    ProductID = pid,
                    ProductName = pname,
                    Price = price,
                    DateOfManufacture = dom
                };

                // Add Product object to collection
                products.Add(product);

                // Ask user if they want to continue
                Console.WriteLine("Product Added Successfully...\n");
                Console.WriteLine("Do you want to continue to next product? (Yes/No)\n");
                choice = Console.ReadLine();

            } while (choice != "No" && choice != "no" && choice != "n" && choice != "N");

            Console.WriteLine("\nProducts: \n");
            foreach (Product item in products)
            {
                Console.WriteLine(item.ProductID + ", " + item.ProductName + ", " + item.Price + ", " + item.DateOfManufacture);
            }

            //========== End Object Collection Tutorial ======================

            // =============== Object Relations Tutorial =======================

                // student class's object
            Student student = new Student();
            student.RollNo = 123;
            student.StudentName = "Josh";
            student.Email = "josh@gmail.com";

            // Branch object // one-to-one relation
            student.branch = new Branch();
            student.branch.BranchName = "ICT Engineering";
            student.branch.NoOfSemester = 8;

            // values
            Console.WriteLine(student.branch.BranchName);
            Console.WriteLine(student.branch.NoOfSemester);
            Console.WriteLine("RollNo: " + student.RollNo);
            Console.WriteLine("Student Name: " + student.StudentName);
            Console.WriteLine("Email: " + student.Email);

            // One-to-many relationship definition
            Student2 student1 = new Student2();
            student1.RollNo = 1;
            student1.StudentName = "Vincent";
            student1.Email = "vincent@gmail";

            student1.examinations = new List<Examination>
            {
                new Examination()
                {
                    ExaminationName = "CSC101",
                    Month = 5,
                    Year = 2023,
                    MaxMarks = 100,
                    SecuredMarks = 87
                },
                new Examination()
                {
                    ExaminationName = "CSC201",
                    Month = 6,
                    Year = 2023,
                    MaxMarks = 100,
                    SecuredMarks = 79
                },
                new Examination()
                {
                    ExaminationName = "MTH404",
                    Month = 8,
                    Year = 2023,
                    MaxMarks = 100,
                    SecuredMarks = 90
                }
            };

            Console.WriteLine("RollNo: " + student1.RollNo);
            Console.WriteLine("Student Name: " + student1.StudentName);
            Console.WriteLine("Email: " + student1.Email);

            foreach (Examination exam in student1.examinations)
            {
                Console.WriteLine(exam.ExaminationName + ", " + exam.Year + ", " + exam.Month + ", " + exam.SecuredMarks + "/" + exam.MaxMarks);
            }


            //Three employees in same department
            Employee employee1 = new Employee() { 
                EmployeeID = 1,
                EmployeeName = "Josh",
                Email = "josh@gmail"
            };
            Employee employee2 = new Employee()
            {
                EmployeeID = 2,
                EmployeeName = "Vincent",
                Email = "vincent@gmail"
            };
            Employee employee3 = new Employee()
            {
                EmployeeID = 3,
                EmployeeName = "David",
                Email = "david@gmail"
            };

            // create object of department class
            Department department1 = new Department()
            {
                DepartmentID = 1,
                DepartmentName = "ICT"
            };

            employee1.dept = department1;
            employee2.dept = department1;
            employee3.dept = department1;

            // print employee details
            Console.WriteLine("\n\nFirst Employee");
            Console.WriteLine("Employee ID: " + employee1.EmployeeID);
            Console.WriteLine("Employee Name: " + employee1.EmployeeName);
            Console.WriteLine("Email: " + employee1.Email);
            Console.WriteLine("Department ID: " + employee1.dept.DepartmentID);
            Console.WriteLine("Department Name: " + employee1.dept.DepartmentName);

            Console.WriteLine("\nSecond Employee");
            Console.WriteLine("Employee ID: " + employee2.EmployeeID);
            Console.WriteLine("Employee Name: " + employee2.EmployeeName);
            Console.WriteLine("Email: " + employee2.Email);
            Console.WriteLine("Department ID: " + employee2.dept.DepartmentID);
            Console.WriteLine("Department Name: " + employee2.dept.DepartmentName);

            Console.WriteLine("\nThird Employee");
            Console.WriteLine("Employee ID: " + employee3.EmployeeID);
            Console.WriteLine("Employee Name: " + employee3.EmployeeName);
            Console.WriteLine("Email: " + employee3.Email);
            Console.WriteLine("Department ID: " + employee3.dept.DepartmentID);
            Console.WriteLine("Department Name: " + employee3.dept.DepartmentName);

            Console.ReadKey();
        }
    }
}
