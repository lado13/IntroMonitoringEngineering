using IntroMonitoringEngineering.Data;
using IntroMonitoringEngineering.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace IntroMonitoringEngineering.Controllers
{
    public class ContextController : Controller
    {

        private readonly ICategoryService _categoryService;
        private readonly IDetailService _informationService;
        private readonly AppDbContext _context;
        private readonly IImageService _imageService;


        public ContextController(ICategoryService categoryService, IDetailService informationService, AppDbContext context, IImageService imageService = null)
        {
            _categoryService = categoryService;
            _informationService = informationService;
            _context = context;
            _imageService = imageService;
        }



        public async Task<IActionResult> Index(int? categoryId)
        {
            var categories = await _categoryService.GetAllCategoriesAsync();
            ViewBag.Categories = categories;

            var img = await _imageService.GetAllImage();
            ViewBag.Images = img;

            if (true)
            {
                ViewBag.Images = img.ToList();
            }
            else
            {
                ViewBag.Images = img;
            }

            var item = await _informationService.GetAllInformationAsync();
         

            if (categoryId.HasValue)
            {
                var selectedCategory = await _categoryService.GetCategoryByIdAsync(categoryId.Value);
                ViewBag.SelectedCategory = selectedCategory;

                var categoryInformation = await _informationService.GetInformationByCategoryIdAsync(categoryId.Value);
                ViewBag.Information = categoryInformation;
            }

            return View(item);
        }


     




        public async Task<IActionResult> About()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();
            ViewBag.Categories = categories;

            return View();

        }







    }
}
