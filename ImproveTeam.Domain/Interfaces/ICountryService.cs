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
        Task<IReadOnlyCollection<City>> GetCitiesAsync(int regionId);
        Task<City> AddCityAsync(int regionId, string name);
        Task UpdateCityAsync(int cityId, string name);
        Task DeleteCityAsync(int cityId);
    }
}
