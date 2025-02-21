using IntroMonitoringEngineering.Data;
using IntroMonitoringEngineering.Interfaces;
using IntroMonitoringEngineering.Models;
using Microsoft.EntityFrameworkCore;

namespace IntroMonitoringEngineering.Services
{
    public class ImageService : IImageService
    {

        private readonly AppDbContext _context;


        public ImageService(AppDbContext context)
        {
            _context = context;
        }


        public async Task<bool> DeleteImageAsync(int imageId)
        {
            var image = await _context.Images.FindAsync(imageId);
            if (image == null)
            {
                return false;
            }


            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", image.Url.TrimStart('/'));
            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }


            _context.Images.Remove(image);
            await _context.SaveChangesAsync();
            return true;
        }




        public async Task<bool> EditImageAsync(int imageId, IFormFile newImageFile, string newAltText = null)
        {
            var image = await _context.Images.FindAsync(imageId);
            if (image == null)
            {
                return false;
            }

            if (newImageFile != null && newImageFile.Length > 0)
            {
                var oldFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", image.Url.TrimStart('/'));
                if (System.IO.File.Exists(oldFilePath))
                {
                    System.IO.File.Delete(oldFilePath);
                }

                var newFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", newImageFile.FileName);
                using (var stream = new FileStream(newFilePath, FileMode.Create))
                {
                    await newImageFile.CopyToAsync(stream);
                }

                image.Url = "/images/" + newImageFile.FileName;
            }

            if (!string.IsNullOrEmpty(newAltText))
            {
                image.AltText = newAltText;
            }

            _context.Images.Update(image);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<ImageUrl>> GetAllImage()
        {
            return await _context.Images.ToListAsync();
        }
    }
}
