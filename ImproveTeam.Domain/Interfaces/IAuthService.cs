using ImproveTeam.Domain.Models.Auth;
using System.Threading.Tasks;

namespace ImproveTeam.Domain.Interfaces
{
    public interface IAuthService
    {
        Task<UserCredentialsVerificationResult> VerifyUserCredentialsAsync(string login, string password);
    }
}
