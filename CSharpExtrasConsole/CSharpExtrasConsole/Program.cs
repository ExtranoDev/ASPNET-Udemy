using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using College;
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
            Console.WriteLine(student.RollNo);
            Console.WriteLine(student.StudentName);
            Console.WriteLine(student.Email);

            Console.ReadKey();
        }
    }
}
