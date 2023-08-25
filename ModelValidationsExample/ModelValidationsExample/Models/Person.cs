using System.ComponentModel.DataAnnotations;

namespace ModelValidationsExample.Models
{
    public class Person
    {
        [Required(ErrorMessage ="Person's name cannot be empty")]
        public string? PersonName { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Password { get; set; }
        public string? ConfirmPassword { get; set; }
        public double? Price { get; set; }
        public override string ToString()
        {
            return $"Person Object ----\nPerson name: " +
                $"{PersonName}\nEmail: {Email}\nPhone: {Phone}\n" +
                $"Password: {Password}\nConfirm Password: {ConfirmPassword}\n" +
                $"Price: {Price}";
        }
    }
}
