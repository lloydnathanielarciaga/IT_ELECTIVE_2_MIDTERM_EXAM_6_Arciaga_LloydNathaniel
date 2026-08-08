using System.ComponentModel.DataAnnotations;

namespace PackagePickupMonitoringSystem.Models
{
    public class User
    {
        public int Id { get; set; } 

        [Required(ErrorMessage = "Please enter your firstname.")] 
        [Display(Name = "First Name")]
        public string FirstName { get; set; } 

        [Required(ErrorMessage = "Please enter your lastname.")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; } 

        [Required]
        [EmailAddress]
        public string Email { get; set; } 

        [Required]
        [StringLength(20, MinimumLength = 5, ErrorMessage = "Username must be minimum of 5 and maximum of 20 characters")] 
        public string Username { get; set; } 

        [Required]
        [DataType(DataType.Password)] 
        public string Password { get; set; } 
    }
}