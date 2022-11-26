using AxlBookCatalog.Business.Abstractions.Services;
using AxlBookCatalog.Business.Models.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace AxlBookCatalog.Controllers
{
    [Route("api/login")]
    [ApiController]
    public class AuthorizationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthorizationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpGet]
        public string Login()
        {
            throw new Exception();
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] AuthenticationRequest request)
        {
            // it's necessary to add middleware to handle all app exceptions
            var response = await _authenticationService.LoginAsync(request);
            if (!string.IsNullOrEmpty(response.RefreshToken))
                SetRefreshTokenInCookie(response.RefreshToken);
            return Ok(response);
        }

        [HttpPost]
        [Route("refreshtoken")]
        public async Task<IActionResult> RefreshToken()
        {
            var refreshToken = Request.Cookies["refreshToken"];
            var response = await _authenticationService.RefreshTokenAsync(refreshToken);
            if (!string.IsNullOrEmpty(response.RefreshToken))
                SetRefreshTokenInCookie(response.RefreshToken);
            return Ok(response);
        }

        private void SetRefreshTokenInCookie(string refreshToken)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddDays(100),
            };

            Response.Cookies.Append("refreshToken", refreshToken, cookieOptions);
        }
    }
}