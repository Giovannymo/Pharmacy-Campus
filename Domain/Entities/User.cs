namespace Domain.Entities;

public class User : BaseEntity
{
    public string Username { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public ICollection<JobTitle> JobsTitle { get; set; } = new HashSet<JobTitle>();
    public ICollection<UserRole> UsersRole { get; set; }
    public ICollection<RefreshToken> RefreshTokens { get; set; }
}
