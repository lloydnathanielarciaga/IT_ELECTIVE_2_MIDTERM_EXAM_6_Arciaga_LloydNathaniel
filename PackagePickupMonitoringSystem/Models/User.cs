using System.ComponentModel.DataAnnotations;

namespace PackagePickupMonitoringSystem.Models
{
    public class User
    {
        public int Id { get; set; } 

        [Required(ErrorMessage = "First Name is required.")] 
        [Display(Name = "First Name")]
        public string FirstName { get; set; } 

        [Required(ErrorMessage = "Last Name is required.")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; } 

        [Required]
        [EmailAddress]
        public string Email { get; set; } 

        [Required]
        [StringLength(20, MinimumLength = 5)] 
        public string Username { get; set; } 

        [Required]
        [DataType(DataType.Password)] 
        public string Password { get; set; } 
    }
}