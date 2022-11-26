using AxlBookCatalog.Business.Abstractions.Services;
using AxlBookCatalog.Business.Models;
using AxlBookCatalog.Business.Models.Authorization;
using AxlBookCatalog.Domain.Constants;
using AxlBookCatalog.Domain.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace AxlBookCatalog.Business.Services
{
    public class RootAdministrationService : IRootAdminstrationService
    {
        private const string SpecifiedEmailNotRegisteredMessage = "The specified email address \"{0}\" is not registered";
        private const string WrongRootAdministratorPasswordMessage = "RootAdministratorPassword is wrong";
        private const string RoleNotExistMessage = "Role \"{0}\" doesn't exist";
        private const string RoleAddedSuccessfulyMessage = "The role \"{0}\" is added to user {1}.";

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;

        public RootAdministrationService(UserManager<ApplicationUser> userManager,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task<CommandOperationResponse<IdentityResult>> SetRoleToUserAsync(SetRoleToUserRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                return new CommandOperationResponse<IdentityResult>()
                {
                    OperatedObject = null,
                    Message = string.Format(SpecifiedEmailNotRegisteredMessage, request.Email),
                    IsSuccess = false
                };
            }

            var rootAdministratorPassword = _configuration["RootAdministratorPassword"];

            if (rootAdministratorPassword.Equals(request.RootAdministratorPassword, StringComparison.OrdinalIgnoreCase))
            {
                var roleExists = Enum.GetNames(typeof(Authorization.Roles))
                    .Any(x => x.ToLower() == request.RoleName.ToLower());

                if (roleExists)
                {
                    var validRole = Enum.GetValues(typeof(Authorization.Roles))
                        .Cast<Authorization.Roles>()
                        .Where(x => x.ToString().ToLower() == request.RoleName.ToLower())
                        .FirstOrDefault();
                    var result = await _userManager.AddToRoleAsync(user, validRole.ToString());

                    return new CommandOperationResponse<IdentityResult>()
                    {
                        OperatedObject = result,
                        Message = string.Format(RoleAddedSuccessfulyMessage, request.RoleName, request.Email),
                        IsSuccess = false
                    };
                }

                return new CommandOperationResponse<IdentityResult>()
                {
                    OperatedObject = null,
                    Message = string.Format(RoleNotExistMessage, request.RoleName),
                    IsSuccess = false
                };
            }

            return new CommandOperationResponse<IdentityResult>()
            {
                OperatedObject = null,
                Message = WrongRootAdministratorPasswordMessage,
                IsSuccess = false
            };
        }
    }
}
