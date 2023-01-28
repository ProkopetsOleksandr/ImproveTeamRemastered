using ImproveTeam.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ImproveTeam.Infrastructure.DataAccess.EF.EntityMappings
{
    public class CityMapping : IEntityTypeConfiguration<City>
    {
        public void Configure(EntityTypeBuilder<City> builder)
        {
            builder.ToTable("Cities");

            builder.HasKey(p => p.Id);

            builder.Property(x => x.Name).HasMaxLength(255);
        }
    }
}
