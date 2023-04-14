using ImproveTeam.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ImproveTeam.Domain.Interfaces
{
    public interface IAdvertiserService
    {
        Task<IReadOnlyCollection<AdvertiserInfo>> GetAdvertisersInfoAsync();
        Task AddAdvertiserAsync(string name, List<int> productIds);
        Task UpdateAdvertiserAsync(int id, string name, List<int> productIds);
        Task DeleteAdvertiserAsync(int advertiserId);
    }
}
