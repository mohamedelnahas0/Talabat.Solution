using System.ComponentModel.DataAnnotations;

namespace Talabat.APIs.DTOS
{
    public class RegisterDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string DisplayName { get; set; }

        [Required]
        [Phone]
        public string PhoneNumber { get; set; }

        [Required]
        [RegularExpression("(?=^.{8,}$)((?=.*\\d)|(?=.*\\W+))(?![.\\n])(?=.*[A-Z])(?=.*[a-z]).*$", 
            ErrorMessage = "Password must contains 1 Uppercase, 1 Lowercase, 1 Digit, 1 Spaecial Character")] 
        public string Password { get; set; }
    }
}
