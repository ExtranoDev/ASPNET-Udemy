using ECommerceFirst.CustomValidator;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace ECommerceFirst.Models
{
    public class Order
    {
        //[Required]
        [MinimumYearValidator()]
        public DateTime OrderDate { get; set; }
        [BindNever]
        public int? OrderNo { get; set; }
        [Required]
        [OrderPriceValidation("Products")]
        public double InvoicePrice { get; set;}
        [Required]
        [MinLength(1)]
        public List<Product> Products { get; set;} = new List<Product>();
    }
}
