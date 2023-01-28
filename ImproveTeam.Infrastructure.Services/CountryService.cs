using ImproveTeam.Domain.Interfaces;
using ImproveTeam.Domain.Models;
using ImproveTeam.Infrastructure.DataAccess.EF;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ImproveTeam.Infrastructure.Services
{
    public class CountryService : ICountryService
    {
        private readonly IDbContext _dbContext;

        public CountryService(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IReadOnlyCollection<Country>> GetCountriesAsync()
        {
            return await _dbContext.Set<Country>()
                .Include(m => m.Regions)
                    .ThenInclude(m => m.Cities)
                .ToListAsync();
        }

        public async Task<Country> GetCountryByIdAsync(int countryId)
        {
            return await _dbContext.Set<Country>()
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == countryId);
        }
    }
}
