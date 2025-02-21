using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace IntroMonitoringEngineering.ViewModel.Detail
{
    public class DetailCreateViewModel
    {
        public string? Description { get; set; }

        [Required(ErrorMessage = "Category is required")]
        public int CategoryId { get; set; }

        public List<SelectListItem> Categories { get; set; } = new List<SelectListItem>();

        public List<IFormFile> Images { get; set; } = new List<IFormFile>();

    }
}
