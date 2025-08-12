using System.ComponentModel.DataAnnotations;

namespace APIServerApp.DTO
{
    public class LoginRequest
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string MatKhau { get; set; }
    }

}