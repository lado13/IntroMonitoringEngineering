using System.ComponentModel.DataAnnotations;

namespace IntroMonitoringEngineering.ViewModel.Account
{
    public class RegisterVM
    {
        [Required]
        public string FullName { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Username { get; set; }

        [Required, DataType(DataType.Password)]
        public string Password { get; set; }

        [Required, DataType(DataType.Password), Compare("Password", ErrorMessage = "Passwords do not match.")]
        public string RepeatPassword { get; set; }
    }
}
