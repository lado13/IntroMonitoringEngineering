using IntroMonitoringEngineering.Data;
using IntroMonitoringEngineering.Interfaces;
using IntroMonitoringEngineering.Models;
using IntroMonitoringEngineering.Services;
using IntroMonitoringEngineering.ViewModel.Detail;
using IntroMonitoringEnginnering.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace IntroMonitoringEngineering.Controllers
{
    public class DetailController : Controller
    {



        private readonly ICategoryService _categoryService;
        private readonly IDetailService _informationService;
        private readonly IImageService _imageService;
        private readonly AppDbContext _context;

        public DetailController(IDetailService informationService, ICategoryService categoryService, AppDbContext context, IImageService imageService)
        {
            _informationService = informationService;
            _categoryService = categoryService;
            _context = context;
            _imageService = imageService;
        }


        public async Task<IActionResult> Search(string searchQuery)
        {
            var details = await _informationService.GetAllInformationAsync(searchQuery);
            return View(details);
        }



        public async Task<IActionResult> Create()
        {
        
            var categories = await _categoryService.GetAllCategoriesAsync();

            var viewModel = new DetailCreateViewModel
            {
                Categories = categories?.Select(c => new SelectListItem
                {
                    Text = c.Title, 
                    Value = c.Id.ToString() 
                }).ToList() ?? new List<SelectListItem>() 
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(DetailCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
             
                var detail = new Detail
                {
                    Description = model.Description,
                    CategoryId = model.CategoryId
                };

             
                await _informationService.AddInformationAsync(detail);

                if (model.Images != null && model.Images.Count > 0)
                {
                    foreach (var imageFile in model.Images)
                    {
                        if (imageFile.Length > 0)
                        {
              
                            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", imageFile.FileName);

                            using (var stream = new FileStream(filePath, FileMode.Create))
                            {
                                await imageFile.CopyToAsync(stream);
                            }

                         
                            var image = new ImageUrl
                            {
                                Url = "/images/" + imageFile.FileName, 
                                AltText = "Image for " + detail.Description,
                                DetailId = detail.Id
                            };

                            await _informationService.AddImageAsync(image);
                        }
                    }
                }
            }

            return View(model);
        }




        public async Task<IActionResult> DeleteInformation(int informationId, int categoryId)
        {
            await _informationService.DeleteInformationAsync(informationId);
            return RedirectToAction("Index", "Context", new { categoryId = categoryId });
        }







        [HttpGet("edit/{id}")]
        public async Task<IActionResult> EditDescription(int id)
        {
            var detail = await _informationService.GetDetailByIdAsync(id);
            if (detail == null)
            {
                return NotFound();
            }

            var viewModel = new DetailViewModel
            {
                Id = detail.Id,
                Description = detail.Description
            };

            return View("EditDescription", viewModel);
        }





        [HttpPost("edit/{id}")]
        public async Task<IActionResult> UpdateDescription(int id, DetailViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("UpdateDescription", model);
            }

            var detail = await _informationService.GetDetailByIdAsync(id);
            if (detail == null)
            {
                return NotFound();
            }

            detail.Description = model.Description;

            var updated = await _informationService.UpdateDetailAsync(detail);
            if (!updated)
            {
                ModelState.AddModelError("", "Failed to update the detail.");
                return View("UpdateDescription", model);
            }

            return RedirectToAction("Index", "Context");
            
        }
















    }


}
