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
    }
}
