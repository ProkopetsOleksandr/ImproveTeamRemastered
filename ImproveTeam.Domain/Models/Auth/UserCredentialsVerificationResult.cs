namespace ImproveTeam.Domain.Models.Auth
{
    public class UserCredentialsVerificationResult
    {
        public User User { get; set; }
        public bool IsUserValid => User != null;
    }
}
