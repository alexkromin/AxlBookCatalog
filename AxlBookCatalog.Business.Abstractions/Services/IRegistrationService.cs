using AxlBookCatalog.Business.Models;
using AxlBookCatalog.Domain.Users;

namespace AxlBookCatalog.Business.Abstractions.Services
{
    public interface IRegistrationService
    {
        Task<CommandOperationResponse<ApplicationUser>> RegisterAsync(RegistrationUserRequest model);
    }
}
