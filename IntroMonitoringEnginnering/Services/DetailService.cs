using IntroMonitoringEngineering.Data;
using IntroMonitoringEngineering.Interfaces;
using IntroMonitoringEngineering.Models;
using IntroMonitoringEnginnering.Models;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;

namespace IntroMonitoringEngineering.Services
{
    public class DetailService : IDetailService
    {

        private readonly AppDbContext _context;

        public DetailService(AppDbContext context)
        {
            _context = context;
        }


        public async Task<IEnumerable<Detail>> GetAllInformationAsync(string searchQuery = null)
        {
            var query = _context.Details.AsQueryable();

            if (!string.IsNullOrEmpty(searchQuery))
            {
                query = query.Where(d => d.Description.Contains(searchQuery));
            }

            return await query.ToListAsync();
        }



        public async Task<IEnumerable<Detail>> GetAllInformationAsync()
        {
            return await _context.Details.ToListAsync();
        }

        public async Task<IEnumerable<Detail>> GetInformationByCategoryIdAsync(int categoryId)
        {
            return await _context.Details
                                 .Where(d => d.CategoryId == categoryId)
                                 .Include(d => d.Image)
                                 .ToListAsync();
        }


        public async Task AddInformationAsync(Detail information)
        {
            await _context.Details.AddAsync(information);
            await _context.SaveChangesAsync();
        }

        //public async Task UpdateInformationAsync(Detail information)
        //{
        //    _context.Details.Update(information);
        //    await _context.SaveChangesAsync();
        //}

        public async Task DeleteInformationAsync(int informationId)
        {
            var information = await _context.Details.FindAsync(informationId);
            if (information != null)
            {
                _context.Details.Remove(information);
                await _context.SaveChangesAsync();
            }
        }



        public async Task AddImageAsync(ImageUrl image)
        {
            _context.Images.Add(image);
            await _context.SaveChangesAsync();
        }

        public async Task<Detail?> GetDetailByIdAsync(int id)
        {
            return await _context.Details.FindAsync(id);
        }

        public async Task<bool> UpdateDetailAsync(Detail detail)
        {
            var existingDetail = await _context.Details.FindAsync(detail.Id);
            if (existingDetail == null)
            {
                return false;
            }

            existingDetail.Description = detail.Description;

            _context.Entry(existingDetail).State = EntityState.Modified;

            await _context.SaveChangesAsync();
            return true;
        }




    }
}
