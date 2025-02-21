using IntroMonitoringEngineering.Data;
using IntroMonitoringEngineering.Interfaces;
using IntroMonitoringEngineering.ViewModel.Image;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IntroMonitoringEngineering.Controllers
{
    public class ImageController : Controller
    {


        private readonly AppDbContext _context;
        private readonly IImageService _imageService;


        public ImageController(AppDbContext context, IImageService imageService)
        {
            _context = context;
            _imageService = imageService;
        }


        [HttpGet]
        public async Task<IActionResult> EditImage(int id)
        {
            var image = await _context.Images.FirstOrDefaultAsync(i => i.Id == id);

            if (image == null)
            {
                return NotFound();
            }

            var model = new EditImageViewModel
            {
                Id = image.Id,
                AltText = image.AltText,
                CurrentImageUrl = image.Url,
                DetailId = image.DetailId
            };

            return View(model);
        }




        [HttpPost]
        public async Task<IActionResult> EditImage(int id, IFormFile newImage, string newAltText)
        {
            var categoryId = await _imageService.EditImageAsync(id, newImage, newAltText);

            if (categoryId == null)
            {
                return NotFound("Image or associated category not found.");
            }
            return RedirectToAction("Index", "Context", new { categoryId = categoryId });
        }




        [HttpGet]
        public async Task<IActionResult> DeleteImage(int id, int categoryId)
        {
            var result = await _imageService.DeleteImageAsync(id);
            if (result)
            {
                return RedirectToAction("Index", "Context", new { categoryId = categoryId });
            }

            return NotFound();
        }






    }
}
