using Microsoft.AspNetCore.Mvc;

namespace ControllersExample.Models
{
    public class Book
    {
        //[FromQuery]
        public int? BookId { get; set; }
        public string? Author { get; set; }
        public override string ToString()
        {
            return $"Book object - Book ID: {BookId}, Author: {Author}";
        }
    }
}
