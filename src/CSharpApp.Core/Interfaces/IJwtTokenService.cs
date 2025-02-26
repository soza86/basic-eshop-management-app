namespace CSharpApp.Core.Interfaces
{
    public interface IJwtTokenService
    {
        Task<string> GetToken();
    }
}
