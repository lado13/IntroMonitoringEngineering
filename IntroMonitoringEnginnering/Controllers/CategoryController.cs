using IntroMonitoringEngineering.Interfaces;
using IntroMonitoringEngineering.ViewModel.Category;
using IntroMonitoringEnginnering.Models;
using Microsoft.AspNetCore.Mvc;

namespace IntroMonitoringEngineering.Controllers
{
    public class CategoryController : Controller
    {

        private readonly ICategoryService _categoryService;
        private readonly IDetailService _informationService;
        private readonly IEmailService _emailService;

        public CategoryController(ICategoryService categoryService, IDetailService informationService, IEmailService emailService)
        {
            _categoryService = categoryService;
            _informationService = informationService;
            _emailService = emailService;
        }



        public async Task<IActionResult> GetAllCategory()
        {
            var item = await _categoryService.GetAllCategoriesAsync();
            return View(item);
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View(); 
        }




        [HttpPost]
        public async Task<IActionResult> Create(CategoryCreate categoryCreate)
        {
            if (ModelState.IsValid)
            {

                var category = new Category
                {
                    Title = categoryCreate.Title
                };

                await _categoryService.AddCategoryAsync(category);
                return RedirectToAction("Index", "Context");
            }

            return View(categoryCreate);
        }


        public async Task<IActionResult> Edit(int id)
        {
            var category = await _categoryService.GetCategoryByIdAsync(id);
            return View(category);
        }


       

        [HttpPost]
        public async Task<IActionResult> Edit(int id, CategoryEdit model)
        {
            if (ModelState.IsValid)
            {
                var category = await _categoryService.GetCategoryByIdAsync(id);
                if (category == null)
                {
                    return NotFound();
                }

                category.Title = model.Title;
                await _categoryService.UpdateCategoryAsync(category);
                return RedirectToAction("Index", "Context");
            }

            ViewBag.CategoryId = id;
            return View(model);
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _categoryService.DeleteCategoryAsync(id);
            return RedirectToAction("Index","Context"); 
        }









    }
}
