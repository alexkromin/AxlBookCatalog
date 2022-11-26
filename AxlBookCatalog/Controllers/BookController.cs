using AxlBookCatalog.Business.Abstractions.Services;
using AxlBookCatalog.Business.Models.Book;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AxlBookCatalog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> AddAsync([FromBody] AddBookRequest request)
        {
            var result = await _bookService.AddAsync(request);

            return Ok(result);
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [Route("remove")]
        public async Task<IActionResult> RemoveAsync([FromBody] string id)
        {
            var result = await _bookService.RemoveAsync(id);

            return Ok(result);
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [Route("edit")]
        public async Task<IActionResult> EditAsync([FromBody] EditBookRequest request)
        {
            var result = await _bookService.EditAsync(request);

            return Ok(result);
        }

        [Authorize(Roles = "User")]
        [HttpGet]
        [Route("getbooksbystring")]
        public async Task<IActionResult> GetBooksByStringAsync(string str)
        {
            var books = await _bookService.GetByStringAsync(str);

            return Ok(books);
        }
    }
}
