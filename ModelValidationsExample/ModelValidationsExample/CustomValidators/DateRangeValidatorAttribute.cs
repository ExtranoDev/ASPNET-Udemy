using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Reflection;

namespace ModelValidationsExample.CustomValidators
{
    public class DateRangeValidatorAttribute : ValidationAttribute
    {
        public string OtherProertryName { get; set; }
        public DateRangeValidatorAttribute(string otherProertryName) { 
            OtherProertryName = otherProertryName;
        }
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value != null)
            {
                // gets to date value
                DateTime to_date = Convert.ToDateTime(value);

                // get from_date value
                PropertyInfo? otherProperty = 
                    validationContext.ObjectType.GetProperty(OtherProertryName);

                if (otherProperty != null)
                {
                    DateTime from_date = Convert.ToDateTime(otherProperty.
                    GetValue(validationContext.ObjectInstance));

                    if (from_date > to_date)
                    {
                        return new ValidationResult(ErrorMessage, new string[]
                        {
                        OtherProertryName,
                        validationContext.MemberName
                        });
                    }
                    else return ValidationResult.Success;
                }
                return null;
            }
            return null;
        }
    }
}
