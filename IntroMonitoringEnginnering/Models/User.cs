using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace IntroMonitoringEngineering.Models
{
    public class User : IdentityUser
    {

        public string FullName { get; set; }

    }
}
