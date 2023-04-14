using ImproveTeam.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ImproveTeam.Domain.Interfaces
{
    public interface IInterestsService
    {
        Task<IReadOnlyCollection<Interest>> GetInterestsAsync();
        Task<Interest> AddInterestAsync(string name);
        Task UpdateInterestAsync(int interestId, string name);
        Task DeleteInterestAsync(int interestId);
    }
}
