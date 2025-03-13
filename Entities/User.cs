using System;
using System.ComponentModel.DataAnnotations;

namespace SalesTest.Entities
{
    public class User
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Username { get; set; } = string.Empty;

        [Required]
        public string PasswordHash { get; set; } = string.Empty;

        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;

        public string City { get; set; } = string.Empty;
        public string Street { get; set; } = string.Empty;
        public int Number { get; set; }
        public string Zipcode { get; set; } = string.Empty;

        public string Phone { get; set; } = string.Empty;

        [Required]
        public string Status { get; set; } = "Active";

        [Required]
        public string Role { get; set; } = "Customer";
    }
}
