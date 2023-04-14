using ImproveTeam.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ImproveTeam.Infrastructure.DataAccess.EF.EntityMappings
{
    public class ApplicationMapping : IEntityTypeConfiguration<Application>
    {
        public void Configure(EntityTypeBuilder<Application> builder)
        {
            builder.ToTable("Applications");

            builder.HasKey(p => p.Id);

            builder.Property(x => x.Name).HasMaxLength(255);

            builder.HasOne(m => m.Seller).WithMany(m => m.Applications);
        }
    }
}
