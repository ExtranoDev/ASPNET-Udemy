using ClassLibrary1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpExtrasConsole
{
    // static class for extension method
    public static class ProductExtensions
    {
        // extension method for product class
        public static double GetDiscount(this Product product)
        {
            return product.ProductCost * product.DiscountPercentage / 100;
        }

        public static string timeConversion(string s)
        {
            string hourFormat = s.Substring(s.Length - 2, 2);
            int newHour = int.Parse(s.Substring(0, 2));

            if (hourFormat == "PM")
            {
                if ((newHour + 12) > 24)
                {
                    return newHour + s.Substring(2, 6);
                }
                return (newHour + 12) + s.Substring(2, 6);
            }
            else
            {
                if ((newHour - 12) < 0)
                {
                    return newHour + s.Substring(2, 6);
                }
                return "00" + s.Substring(2, 6);
            }
        }
    }
}
