
namespace ProjectRegistrationSystem.Services.Interfaces
{
    public interface IJwtService
    {
        string GetJwtToken(string username, string role, Guid userId);
    }
}