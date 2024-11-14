using System;
using System.ComponentModel.DataAnnotations;

namespace LAB_TASK.DTOs
{
    public class CustomerDTO
    {
        public int CustomerId { get; set; }

        [Required(ErrorMessage = "First Name is required.")]
        [StringLength(50, ErrorMessage = "First Name cannot exceed 50 characters.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required.")]
        [StringLength(50, ErrorMessage = "Last Name cannot exceed 50 characters.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid Email Format.")]
        public string Email { get; set; }

        [Phone(ErrorMessage = "Invalid Phone Number.")]
        [StringLength(15, MinimumLength = 10, ErrorMessage = "Phone number must be between 10 and 15 digits.")]
        public string Phone { get; set; }

        [StringLength(250, ErrorMessage = "Address cannot exceed 250 characters.")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Date Joined is required.")]
        [DataType(DataType.Date)]
        [CustomValidation(typeof(CustomerDTO), nameof(ValidateDateNotInFuture))]
        public DateTime DateJoined { get; set; }

        
        public static ValidationResult ValidateDateNotInFuture(DateTime date, ValidationContext context)
        {
            if (date > DateTime.Now)
            {
                return new ValidationResult("Date Joined cannot be in the future.");
            }
            return ValidationResult.Success;
        }
    }
}
