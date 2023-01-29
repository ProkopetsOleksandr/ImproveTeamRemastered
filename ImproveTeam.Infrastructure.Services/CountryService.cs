using ImproveTeam.Domain.Interfaces;
using ImproveTeam.Domain.Models;
using ImproveTeam.Infrastructure.DataAccess.EF;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<IReadOnlyCollection<Region>> GetRegionsAsync(int countryId)
        {
            return await _dbContext.Set<Region>()
                .AsNoTracking()
                .Where(m => m.CountryId == countryId)
                .ToListAsync();
        }

        public async Task<Region> AddRegionAsync(int countryId, string regionName)
        {
            var region = new Region
            {
                CountryId = countryId,
                Name = regionName
            };

            _dbContext.Set<Region>().Add(region);
            await _dbContext.SaveChangesAsync();

            return region;
        }

        public async Task UpdateRegionAsync(int regionId, string name)
        {
            var region = await _dbContext.Set<Region>().FindAsync(regionId);
            if (region == null)
            {
                return;
            }

            region.Name = name;
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteRegionAsync(int regionId)
        {
            var region = new Region { Id = regionId };
            
            _dbContext.Set<Region>().Attach(region);
            _dbContext.Set<Region>().Remove(region);

            await _dbContext.SaveChangesAsync();
        }

        public async Task<IReadOnlyCollection<City>> GetCitiesAsync(int regionId)
        {
            return await _dbContext.Set<City>()
                .Where(m => m.RegionId == regionId)
                .ToListAsync();
        }

        public async Task<City> AddCityAsync(int regionId, string cityName)
        {
            var city = new City
            {
                RegionId = regionId,
                Name = cityName
            };

            _dbContext.Set<City>().Add(city);
            await _dbContext.SaveChangesAsync();

            return city;
        }

        public async Task UpdateCityAsync(int cityId, string name)
        {
            var city = await _dbContext.Set<City>().FindAsync(cityId);
            if (city == null)
            {
                return;
            }

            city.Name = name;
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteCityAsync(int cityId)
        {
            var city = new City { Id = cityId };

            _dbContext.Set<City>().Attach(city);
            _dbContext.Set<City>().Remove(city);

            await _dbContext.SaveChangesAsync();
        }
    }
}
