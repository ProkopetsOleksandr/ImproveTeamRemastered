using ImproveTeam.Domain.Interfaces;
using ImproveTeam.Domain.Models;
using ImproveTeam.Infrastructure.DataAccess.EF;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ImproveTeam.Infrastructure.Services
{
    public class InterestsService : IInterestsService
    {
        private readonly IDbContext _dbContext;

        public InterestsService(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IReadOnlyCollection<Interest>> GetInterestsAsync()
        {
            return await _dbContext.Set<Interest>().ToListAsync();
        }

        public async Task<Interest> AddInterestAsync(string name)
        {
            var interest = new Interest
            {
                Name = name
            };

            _dbContext.Set<Interest>().Add(interest);
            await _dbContext.SaveChangesAsync();

            return interest;
        }

        public async Task UpdateInterestAsync(int interestId, string name)
        {
            var interest = await _dbContext.Set<Interest>().FindAsync(interestId);
            if (interest == null)
            {
                return;
            }

            interest.Name = name;
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteInterestAsync(int interestId)
        {
            var interest = new Interest { Id = interestId };

            _dbContext.Set<Interest>().Attach(interest);
            _dbContext.Set<Interest>().Remove(interest);

            await _dbContext.SaveChangesAsync();
        }
    }
}
