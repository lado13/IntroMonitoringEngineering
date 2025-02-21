using IntroMonitoringEngineering.Models;
using IntroMonitoringEnginnering.Models;
using static System.Net.Mime.MediaTypeNames;

namespace IntroMonitoringEngineering.Interfaces
{
    public interface IDetailService
    {
        Task<IEnumerable<Detail>> GetAllInformationAsync(string searchQuery = null);
        Task<IEnumerable<Detail>> GetAllInformationAsync();
        Task<IEnumerable<Detail>> GetInformationByCategoryIdAsync(int categoryId);
        Task AddInformationAsync(Detail information);
        Task AddImageAsync(ImageUrl image);
        //Task UpdateInformationAsync(Detail information);
        Task DeleteInformationAsync(int informationId);
        Task<Detail?> GetDetailByIdAsync(int id);
        Task<bool> UpdateDetailAsync(Detail detail);
    }
}
