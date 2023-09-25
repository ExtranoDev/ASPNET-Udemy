using ECommerceFirst.Models;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace ECommerceFirst.CustomValidator
{
    public class OrderPriceValidationAttribute : ValidationAttribute
    {
        public string OtherPropertyName { get; set; }
        public string DefaultErrorMessage { get; set; } = "InvoicePrice doesn't match with the total cost of the specified products in the order.";

        public OrderPriceValidationAttribute(string otherPropertyName) { 
            OtherPropertyName = otherPropertyName;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value != null)
            {
                decimal price = Convert.ToDecimal(value);
                PropertyInfo? productProperty = validationContext.ObjectType.GetProperty(OtherPropertyName);

                if (productProperty != null)
                {
                    List<Product>? products = productProperty.GetValue(validationContext.ObjectInstance) as List<Product>;
                    

                    if (products != null)
                    {
                        decimal totalPrice = 0;
                        foreach (var product in products)
                        {
                            totalPrice += Convert.ToDecimal(product.Price * product.Quantity);
                        }
                        if (totalPrice != price)
                            //return new ValidationResult(ErrorMessage, new string[]
                            //{
                            //    OtherPropertyName,
                            //    validationContext.MemberName
                            //});
                            return new ValidationResult(string.Format(ErrorMessage ?? DefaultErrorMessage));
                        else
                            return ValidationResult.Success;
                    }
                }
                return null;
            }
            return null;
        }
    }
}
