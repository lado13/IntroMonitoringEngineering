using IntroMonitoringEnginnering.Models;
using System.ComponentModel.DataAnnotations;

namespace IntroMonitoringEngineering.Models
{
    public class ImageUrl
    {

        [Key]
        public int Id { get; set; }

        public string Url { get; set; }  
        public string AltText { get; set; } 

        public int DetailId { get; set; }  
        public Detail Detail { get; set; }  

    }
}
