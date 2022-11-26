using AxlBookCatalog.Business.Abstractions.Services;
using AxlBookCatalog.Business.Models.Authentication;
using AxlBookCatalog.DataAccess.Contexts;
using AxlBookCatalog.Domain.Users;
using AxlBookCatalog.Utils;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace AxlBookCatalog.Business.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private const string WrongLoginOrPasswordMessage = "Login or password is invalid";
        private const string NotExistedTokenMessage = "Token doesn't exist";
        private const string InactiveTokenMessage = "Token is inactive";

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly JwtSettings _jwtSettings;
        private readonly AxlIdentityDbContext _context;

        public AuthenticationService(UserManager<ApplicationUser> userManager,
            IOptions<JwtSettings> jwtSettings)
        {
            _userManager = userManager;
            _jwtSettings = jwtSettings.Value;            
        }

        public async Task<AuthenticationResponse> LoginAsync(AuthenticationRequest request)
        {
            var result = new AuthenticationResponse();
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                result.IsAuthenticated = false;
                result.Message = WrongLoginOrPasswordMessage;
                return result;
            }

            if (await _userManager.CheckPasswordAsync(user, request.Password))
            {
                result.IsAuthenticated = true;
                var jwtSecurityToken = await CreateJwtTokenAsync(user);
                result.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
                result.Email = user.Email;

                if (user.RefreshTokens.Any(a => a.IsActive))
                {
                    var activeRefreshToken = user.RefreshTokens
                        .Where(a => a.IsActive == true)
                        .FirstOrDefault();

                    result.RefreshToken = activeRefreshToken.Token;
                    result.DateOfExpirationRefreshToken = activeRefreshToken.Expires;
                }
                else
                {
                    var refreshToken = CreateRefreshToken();
                    result.RefreshToken = refreshToken.Token;
                    result.DateOfExpirationRefreshToken = refreshToken.Expires;
                    user.RefreshTokens.Add(refreshToken);
                    _context.Update(user);
                    _context.SaveChanges();
                }

                return result;
            }

            result.IsAuthenticated = false;
            result.Message = WrongLoginOrPasswordMessage;

            return result;
        }

        public async Task<AuthenticationResponse> RefreshTokenAsync(string token)
        {
            var authenticationModel = new AuthenticationResponse();
            var user = _context.Users.SingleOrDefault(u => u.RefreshTokens.Any(t => t.Token == token));
            if (user == null)
            {
                authenticationModel.IsAuthenticated = false;
                authenticationModel.Message = NotExistedTokenMessage;

                return authenticationModel;
            }

            var refreshToken = user.RefreshTokens.Single(x => x.Token == token);
            if (!refreshToken.IsActive)
            {
                authenticationModel.IsAuthenticated = false;
                authenticationModel.Message = InactiveTokenMessage;

                return authenticationModel;
            }

            refreshToken.Revoked = DateTime.UtcNow;

            var newRefreshToken = CreateRefreshToken();
            user.RefreshTokens.Add(newRefreshToken);
            _context.Update(user);
            _context.SaveChanges();

            authenticationModel.IsAuthenticated = true;
            var jwtSecurityToken = await CreateJwtTokenAsync(user);
            authenticationModel.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            authenticationModel.Email = user.Email;
            var rolesList = await _userManager.GetRolesAsync(user).ConfigureAwait(false);
            authenticationModel.Roles = rolesList.ToList();
            authenticationModel.RefreshToken = newRefreshToken.Token;
            authenticationModel.DateOfExpirationRefreshToken = newRefreshToken.Expires;

            return authenticationModel;
        }

        private async Task<JwtSecurityToken> CreateJwtTokenAsync(ApplicationUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);
            var roleClaims = new List<Claim>();
            for (int i = 0; i < roles.Count; i++)
            {
                roleClaims.Add(new Claim("roles", roles[i]));
            }

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("uid", user.Id)
            }
            .Union(userClaims)
            .Union(roleClaims);
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                notBefore: DateTime.Now,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.LifetimeInMinutes),
                signingCredentials: signingCredentials);
            return jwtSecurityToken;
        }

        private RefreshToken CreateRefreshToken()
        {
            var randomNumber = new byte[64];
            using (var generator = new RNGCryptoServiceProvider())
            {
                generator.GetBytes(randomNumber);
                return new RefreshToken
                {
                    Token = Convert.ToBase64String(randomNumber),
                    Expires = DateTime.UtcNow.AddDays(1),
                    Created = DateTime.UtcNow
                };
            }
        }
    }
}