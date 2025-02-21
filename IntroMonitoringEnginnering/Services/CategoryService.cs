using IntroMonitoringEngineering.Data;
using IntroMonitoringEngineering.Interfaces;
using IntroMonitoringEnginnering.Models;
using Microsoft.EntityFrameworkCore;

namespace IntroMonitoringEngineering.Services
{
    public class CategoryService : ICategoryService
    {

        private readonly AppDbContext _context;

        public CategoryService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<Category> GetCategoryByIdAsync(int categoryId)
        {
            return await _context.Categories
                                 .Include(c => c.Information)
                                 .FirstOrDefaultAsync(c => c.Id == categoryId);
        }

        public async Task AddCategoryAsync(Category category)
        {
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCategoryAsync(Category category)
        {
            _context.Categories.Update(category);
            await _context.SaveChangesAsync();
        }


        public async Task DeleteCategoryAsync(int categoryId)
        {
            var category = await _context.Categories
                .Include(c => c.Information) 
                .FirstOrDefaultAsync(c => c.Id == categoryId);

            if (category == null)
            {
                throw new KeyNotFoundException($"კატეგორია ID {categoryId}-ით ვერ მოიძებნა.");
            }

            _context.Details.RemoveRange(category.Information);
            _context.Categories.Remove(category);

            await _context.SaveChangesAsync();
        }







    }
}
