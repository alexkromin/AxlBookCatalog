using AxlBookCatalog.Business.Abstractions.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AxlBookCatalog.Controllers
{
    [Authorize(Roles = "User")]
    [Route("api/[controller]")]
    [ApiController]
    public class BookShelfController : ControllerBase
    {
        private readonly IBookShelfService _bookShelfService;

        public BookShelfController(IBookShelfService bookShelfService)
        {
            _bookShelfService = bookShelfService;
        }

        [Authorize(Roles = "User")]
        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> AddAsync(string bookId)
        {
            await _bookShelfService.AddAsync(bookId);

            return Ok();
        }
    }
}
