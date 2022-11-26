using AxlBookCatalog.Business.Abstractions.Services;
using AxlBookCatalog.Business.Models.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AxlBookCatalog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RootAdministrationController : ControllerBase
    {
        private readonly IRootAdminstrationService _rootAdminstrationService;

        public RootAdministrationController(IRootAdminstrationService rootAdminstrationService)
        {
            _rootAdminstrationService = rootAdminstrationService;
        }

        [HttpPost]
        public async Task<IActionResult> SetRoleToUser(SetRoleToUserRequest request)
        {
            await _rootAdminstrationService.SetRoleToUserAsync(request);
            return Ok();
        }
    }
}
