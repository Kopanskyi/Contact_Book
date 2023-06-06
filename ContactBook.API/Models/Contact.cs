using System;

namespace ContactBook.API.Models
{
    public class Contact
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public int CountryId { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
