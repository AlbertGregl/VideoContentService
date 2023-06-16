using System.ComponentModel.DataAnnotations;

namespace IntegrationModule.Models
{
    public class ChangePasswordRequest
    {
        public int Id { get; set; }
        public string Username { get; set; }

        [Required]
        public string CurrentPassword { get; set; }

        [Required]
        public string NewPassword { get; set; }

    }
}
