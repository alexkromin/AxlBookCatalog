using AxlBookCatalog.Business.Abstractions.Services;
using AxlBookCatalog.Business.Models;
using AxlBookCatalog.Domain.Constants;
using AxlBookCatalog.Domain.Users;
using AxlBookCatalog.Utils;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace AxlBookCatalog.Business.Services
{
    public class RegistrationService : IRegistrationService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly JwtSettings _jwtSettings;

        public RegistrationService(UserManager<ApplicationUser> userManager, 
            RoleManager<IdentityRole> roleManager,
            IOptions<JwtSettings> jwtSettings)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _jwtSettings = jwtSettings.Value;
        }

        public async Task<CommandOperationResponse<ApplicationUser>> RegisterAsync(RegistrationUserRequest model)
        {
            var user = new ApplicationUser
            {
                UserName = model.Username,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName
            };
            var userWithSameEmail = await _userManager.FindByEmailAsync(model.Email);

            if (userWithSameEmail == null)
            {
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    var res = await _userManager.AddToRoleAsync(user, Authorization.DefaultRole.ToString());
                }

                return new CommandOperationResponse<ApplicationUser>()
                {
                    OperatedObject = user,
                    Message = $"The user with username {user.UserName} has been registered",
                    IsSuccess = true
                };                
            }
            else
            {
                return new CommandOperationResponse<ApplicationUser>()
                {
                    OperatedObject = user,
                    Message = $"The user with email \"{user.Email}\" has already been registered.",
                    IsSuccess = false
                };
            }
        }
    }
}
