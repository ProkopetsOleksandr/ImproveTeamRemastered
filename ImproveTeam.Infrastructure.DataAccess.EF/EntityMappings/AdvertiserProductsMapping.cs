using ImproveTeam.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ImproveTeam.Infrastructure.DataAccess.EF.EntityMappings
{
    public class AdvertiserProductsMapping : IEntityTypeConfiguration<AdvertiserProducts>
    {
        public void Configure(EntityTypeBuilder<AdvertiserProducts> builder)
        {
            builder.ToTable("AdvertiserProducts");

            builder.HasKey(p => new { p.AdvertiserId, p.ProductId });
        }
    }
}
