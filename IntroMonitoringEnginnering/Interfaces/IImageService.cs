using IntroMonitoringEngineering.Models;

namespace IntroMonitoringEngineering.Interfaces
{
    public interface IImageService
    {
        Task<bool> EditImageAsync(int imageId, IFormFile newImageFile, string newAltText = null);
        Task<bool> DeleteImageAsync(int imageId);
        Task<IEnumerable<ImageUrl>> GetAllImage();
    }
}
