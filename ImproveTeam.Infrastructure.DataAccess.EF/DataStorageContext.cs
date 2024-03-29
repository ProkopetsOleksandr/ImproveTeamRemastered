﻿using ImproveTeam.Domain.Models;
using ImproveTeam.Infrastructure.DataAccess.EF.EntityMappings;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ImproveTeam.Infrastructure.DataAccess.EF
{
    public class DataStorageContext : DbContext, IDbContext
    {
        public DataStorageContext(DbContextOptions<DataStorageContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Advertiser> Advertisers { get; set; }
        public DbSet<AdvertiserProducts> AdvertiserProducts { get; set; }
        public DbSet<Interest> Interests { get; set; }
        public DbSet<Seller> Sellers { get; set; }
        public DbSet<Application> Applications { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserMapping());
            modelBuilder.ApplyConfiguration(new CountryMapping());
            modelBuilder.ApplyConfiguration(new RegionMapping());
            modelBuilder.ApplyConfiguration(new CityMapping());
            modelBuilder.ApplyConfiguration(new ProductMapping());
            modelBuilder.ApplyConfiguration(new AdvertiserMapping());
            modelBuilder.ApplyConfiguration(new AdvertiserProductsMapping());
            modelBuilder.ApplyConfiguration(new InterestMapping());
            modelBuilder.ApplyConfiguration(new SellerMapping());
            modelBuilder.ApplyConfiguration(new ApplicationMapping());
        }

        public async Task<int> SaveChangesAsync()
        {
            return await base.SaveChangesAsync();
        }
    }
}
