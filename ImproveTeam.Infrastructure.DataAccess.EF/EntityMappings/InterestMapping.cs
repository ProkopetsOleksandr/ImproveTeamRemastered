using ImproveTeam.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ImproveTeam.Infrastructure.DataAccess.EF.EntityMappings
{
    public class InterestMapping : IEntityTypeConfiguration<Interest>
    {
        public void Configure(EntityTypeBuilder<Interest> builder)
        {
            builder.ToTable("Interests");

            builder.HasKey(p => p.Id);

            builder.Property(x => x.Name).HasMaxLength(255);
        }
    }
}
