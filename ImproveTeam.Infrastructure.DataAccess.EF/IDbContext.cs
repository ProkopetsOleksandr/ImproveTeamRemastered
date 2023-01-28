using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ImproveTeam.Infrastructure.DataAccess.EF
{
    public interface IDbContext
    {
        DbSet<T> Set<T>() where T : class;
        Task<int> SaveChangesAsync();
    }
}
