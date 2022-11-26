using AxlBookCatalog.Business.Abstractions.Services;
using AxlBookCatalog.Business.Models;
using Microsoft.AspNetCore.Mvc;

namespace AxlBookCatalog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {
        private readonly IRegistrationService _registrationService;

        public RegistrationController(IRegistrationService registrationService)
        {
            _registrationService = registrationService;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegistrationUserRequest request)
        {
            var result = await _registrationService.RegisterAsync(request);
            return Ok(result);
        }
    }
}
