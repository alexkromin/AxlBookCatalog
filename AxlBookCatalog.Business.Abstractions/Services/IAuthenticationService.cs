using AxlBookCatalog.Business.Models.Authentication;

namespace AxlBookCatalog.Business.Abstractions.Services
{
    public interface IAuthenticationService
    {
        Task<AuthenticationResponse> LoginAsync(AuthenticationRequest model);

        Task<AuthenticationResponse> RefreshTokenAsync(string token);
    }
}
