using System;
using System.ComponentModel.DataAnnotations;

namespace Mocktails.DAL.Model
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "First Name")]
        [Required(ErrorMessage = "First name is required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "First name must be between 3 and 50 characters")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "Last name is required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Last name must be between 3 and 50 characters")]
        public string LastName { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address format")]
        [StringLength(150, ErrorMessage = "Email must be a maximum of 150 characters")]
        public string Email { get; set; }

        //[Required(ErrorMessage = "Password is required")]
        //public string PasswordHash { get; set; }

        //[Required(ErrorMessage = "Role is required")]
        //public UserRole Role { get; set; }
    }

    //// Enum to define roles for users
    //public enum UserRole
    //{
    //    Admin = 1,
    //    Customer = 2,
    //    Moderator = 3
    //}
}
