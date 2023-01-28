using ImproveTeam.Domain.Interfaces;
using ImproveTeam.Domain.Models;
using ImproveTeam.Domain.Models.Auth;
using ImproveTeam.Infrastructure.DataAccess.EF;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ImproveTeam.Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly IDbContext _dbContext;
        private readonly PasswordHasher<User> _passwordHasher;

        public AuthService(IDbContext dbContext)
        {
            _dbContext = dbContext;
            _passwordHasher = new PasswordHasher<User>();
        }

        public async Task<UserCredentialsVerificationResult> VerifyUserCredentialsAsync(string login, string password)
        {
            var user = await _dbContext.Set<User>()
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Login == login);

            if (user == null ||
                !user.IsActive ||
                _passwordHasher.VerifyHashedPassword(user, user.Password, password) == PasswordVerificationResult.Failed)
            {
                return new UserCredentialsVerificationResult();
            }

            return new UserCredentialsVerificationResult
            {
                User = user
            };
        }
    }
}
