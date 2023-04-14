using ImproveTeam.Domain.Interfaces;
using ImproveTeam.Domain.Models;
using ImproveTeam.Infrastructure.DataAccess.EF;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ImproveTeam.Infrastructure.Services
{
    public class AdvertiserService : IAdvertiserService
    {
        private readonly IDbContext _dbContext;

        public AdvertiserService(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IReadOnlyCollection<AdvertiserInfo>> GetAdvertisersInfoAsync()
        {
            var advertisers = await _dbContext.Set<Advertiser>().ToListAsync();
            var advertiserIds = advertisers.Select(m => m.Id);

            var advertiserProducts = await _dbContext.Set<AdvertiserProducts>()
                .Where(m => advertiserIds.Contains(m.AdvertiserId))
                .ToListAsync();

            var advertiserProductsIdsDictionary = advertiserProducts
                .GroupBy(m => m.AdvertiserId)
                .ToDictionary(m => m.Key, m => m.Select(m => m.ProductId).ToList());

            return advertisers.Select(advertiser => new AdvertiserInfo
            {
                Id = advertiser.Id,
                Name = advertiser.Name,
                ProductIds = advertiserProductsIdsDictionary.ContainsKey(advertiser.Id) ? advertiserProductsIdsDictionary[advertiser.Id] : new List<int>()
            }).ToList();
        }

        public async Task AddAdvertiserAsync(string name, List<int> productIds)
        {
            var advertiser = new Advertiser
            {
                Name = name
            };

            _dbContext.Set<Advertiser>().Add(advertiser);
            await _dbContext.SaveChangesAsync();

            if (productIds != null && productIds.Count > 0)
            {
                foreach (var productId in productIds)
                {
                    _dbContext.Set<AdvertiserProducts>().Add(new AdvertiserProducts
                    {
                        AdvertiserId = advertiser.Id,
                        ProductId = productId
                    });
                }

                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task UpdateAdvertiserAsync(int advertiserId, string name, List<int> productIds)
        {
            var advertiser = await _dbContext.Set<Advertiser>().FindAsync(advertiserId);
            if (advertiser == null)
            {
                return;
            }

            if (advertiser.Name != name)
            {
                advertiser.Name = name;
                await _dbContext.SaveChangesAsync();
            }

            var advertiserProducts = await _dbContext.Set<AdvertiserProducts>()
                .Where(m => m.AdvertiserId == advertiserId)
                .ToListAsync();

            if (advertiserProducts.Count > 0 && (productIds == null || productIds.Count == 0))
            {
                _dbContext.Set<AdvertiserProducts>().RemoveRange(advertiserProducts);
                await _dbContext.SaveChangesAsync();
                return;
            }

            var advertiserProductIds = advertiserProducts.Select(m => m.ProductId);

            var productsToRemove = advertiserProducts.Where(m => !productIds.Contains(m.ProductId)).ToList();
            var productIdsToAdd = productIds.Where(m => !advertiserProductIds.Contains(m)).ToList();

            var hasProductsToRemove = productsToRemove.Count > 0;
            var hasProductsToAdd = productIdsToAdd.Count > 0;

            if (!hasProductsToRemove && !hasProductsToAdd)
            {
                return;
            }

            if (hasProductsToRemove)
            {
                _dbContext.Set<AdvertiserProducts>().RemoveRange(productsToRemove);
            }

            if (hasProductsToAdd)
            {
                foreach (var productId in productIdsToAdd)
                {
                    _dbContext.Set<AdvertiserProducts>().Add(new AdvertiserProducts
                    {
                        AdvertiserId = advertiserId,
                        ProductId = productId
                    });
                }
            }

            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAdvertiserAsync(int advertiserId)
        {
            var advertiser = new Advertiser { Id = advertiserId };
            _dbContext.Set<Advertiser>().Attach(advertiser);
            _dbContext.Set<Advertiser>().Remove(advertiser);

            var advertiserProducts = await _dbContext.Set<AdvertiserProducts>()
                .Where(m => m.AdvertiserId == advertiserId)
                .ToListAsync();

            if(advertiserProducts.Count > 0)
            {
                _dbContext.Set<AdvertiserProducts>().RemoveRange(advertiserProducts);
            }

            await _dbContext.SaveChangesAsync();
        }
    }
}
