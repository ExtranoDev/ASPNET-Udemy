using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace MyConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            int age = 23;
            Console.WriteLine(age);
            Console.WriteLine(int.MaxValue);
            Console.WriteLine(int.MinValue);

            long bigNumber = 90000000L;
            Console.WriteLine(bigNumber);
            Console.WriteLine(long.MaxValue);
            Console.WriteLine(long.MinValue);

            double negative = -55.20D;
            Console.WriteLine(negative);
            Console.WriteLine(double.MaxValue);
            Console.WriteLine(double.MinValue);

            Console.WriteLine(age.ToString("C", CultureInfo.CurrentCulture));
            Console.WriteLine(age.ToString("C", CultureInfo.CreateSpecificCulture("en-NG")));
            Console.Write("Tell me, what's the color of your problem in numbers: ");

            string tellMe = Console.ReadLine();
            int.TryParse(tellMe, out age);

            Console.ReadLine();
        }
    }
}
