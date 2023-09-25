using System.ComponentModel.DataAnnotations;

namespace ECommerceFirst.Models
{
    public class Product
    {
        [Required(ErrorMessage = "{0} cannot be empty")]
        [Range(1,9999)]
        [Display(Name = "Product Code")]
        public int ProductCode { get; set; }
        [Required(ErrorMessage = "{0} cannot be empty")]
        [Range(5, 9999)]
        public double Price { get; set; }
        [Required(ErrorMessage = "{0} cannot be empty")]
        [Range(1, 9999)]
        public int Quantity { get; set; }
    }
}
