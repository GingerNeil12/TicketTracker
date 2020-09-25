using System.ComponentModel.DataAnnotations;

namespace TicketTracker.Shared.ViewModels.Security
{
    public class LoginDto
    {
        [Required]
        [EmailAddress]
        public string EmailAddress { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
