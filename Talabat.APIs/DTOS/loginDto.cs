using System.ComponentModel.DataAnnotations;

namespace Talabat.APIs.DTOS
{
    public class loginDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string password { get; set; }
    }
}
