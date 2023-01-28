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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserMapping());
        }

        public async Task<int> SaveChangesAsync()
        {
            return await base.SaveChangesAsync();
        }
    }
}