using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.X509Certificates;

namespace ModelValidationsExample.CustomValidators
{
    public class MinimumYearValidatorAttribute : ValidationAttribute
    {
        /// <summary>
        /// Custom validation attribute is created
        /// </summary>
        /// <param name="value"></param>
        /// <param name="validationContext"></param>
        /// <returns></returns>
        /// 
        public int MinimumYear { get; set; } = 2000;
        public string DefaultErrorMessage { get; set; } = "Year cannot be less than {0}";
        public MinimumYearValidatorAttribute() { }
        // parameterized constructor
        public MinimumYearValidatorAttribute(int minimumYear) {
            MinimumYear = minimumYear;
        }
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value != null)
            {
                DateTime date = (DateTime)value;
                if (date.Year < MinimumYear)
                {
                    return new ValidationResult(string.Format(ErrorMessage ?? DefaultErrorMessage, MinimumYear));
                } else
                {
                    return ValidationResult.Success;
                }
            }
            return null;
        }
    }
}
