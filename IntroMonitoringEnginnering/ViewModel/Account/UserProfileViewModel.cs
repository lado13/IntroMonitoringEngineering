using System.ComponentModel.DataAnnotations;

namespace IntroMonitoringEngineering.ViewModel.Account
{
    public class UserProfileViewModel
    {
        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Username")]
        public string Username { get; set; }

    }
}
