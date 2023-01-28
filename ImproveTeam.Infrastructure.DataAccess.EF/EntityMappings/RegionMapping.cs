using ImproveTeam.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ImproveTeam.Infrastructure.DataAccess.EF.EntityMappings
{
    public class RegionMapping : IEntityTypeConfiguration<Region>
    {
        public void Configure(EntityTypeBuilder<Region> builder)
        {
            builder.ToTable("Regions");

            builder.HasKey(p => p.Id);

            builder.Property(x => x.Name).HasMaxLength(255);
        }
    }
}
