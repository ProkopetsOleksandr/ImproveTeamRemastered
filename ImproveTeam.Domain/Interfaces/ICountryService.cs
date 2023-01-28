using ImproveTeam.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ImproveTeam.Domain.Interfaces
{
    public interface ICountryService
    {
        Task<IReadOnlyCollection<Country>> GetCountriesAsync();
        Task<Country> GetCountryByIdAsync(int countryId);
    }
}
