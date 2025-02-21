using IntroMonitoringEngineering.Models;
using System.ComponentModel.DataAnnotations;
using static System.Net.Mime.MediaTypeNames;

namespace IntroMonitoringEnginnering.Models
{
    public class Detail
    {

        [Key]
        public int Id { get; set; }

        public string? Description { get; set; }

        public int? CategoryId { get; set; }  
        public Category Category { get; set; }  

        public ICollection<ImageUrl> Image { get; set; }  


   




    }
}
