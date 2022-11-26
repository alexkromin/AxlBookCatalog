using AxlBookCatalog.Business.Models;
using AxlBookCatalog.Business.Models.Authorization;
using Microsoft.AspNetCore.Identity;

namespace AxlBookCatalog.Business.Abstractions.Services
{
    public interface IRootAdminstrationService
    {
        public Task<CommandOperationResponse<IdentityResult>> SetRoleToUserAsync(SetRoleToUserRequest request);
    }
}
