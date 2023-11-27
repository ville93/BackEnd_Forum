namespace MyChat.Services
{
    public interface IJwtHandler
    {
        string GenerateToken(string userId, string email);
    }
}
