using System.ComponentModel.DataAnnotations;
using static System.Net.Mime.MediaTypeNames;

namespace IntroMonitoringEnginnering.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public ICollection<Detail> Information { get; set; }

    }
}
