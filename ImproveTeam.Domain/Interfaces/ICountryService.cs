using ImproveTeam.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ImproveTeam.Domain.Interfaces
{
    public interface ICountryService
    {
        Task<IReadOnlyCollection<Country>> GetCountriesAsync();
        Task<Country> GetCountryByIdAsync(int countryId);
        Task<IReadOnlyCollection<Region>> GetRegionsAsync(int countryId);
        Task<Region> AddRegionAsync(int countryId, string regionName);
        Task UpdateRegionAsync(int regionId, string name);
        Task DeleteRegionAsync(int regionId);
    }
}
