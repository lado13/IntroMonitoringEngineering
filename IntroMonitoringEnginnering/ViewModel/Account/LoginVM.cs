using System.ComponentModel.DataAnnotations;

namespace IntroMonitoringEngineering.ViewModel.Account
{
    public class LoginVM
    {
        [Required, EmailAddress]
        public string Email { get; set; }

        [Required, DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
