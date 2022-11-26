using AxlBookCatalog.Business.Abstractions.Services;
using AxlBookCatalog.DataAccess.Abstractions.Repositories;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace AxlBookCatalog.Business.Services
{
    public class BookShelfService : IBookShelfService
    {
        private readonly IUserRepository _userRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public BookShelfService(IUserRepository userRepository, IHttpContextAccessor httpContextAccessor)
        {
            _userRepository = userRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task AddAsync(string bookId)
        {
            var currentUserId = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (currentUserId != null)
                await _userRepository.AddToFavouritesAsync(currentUserId, bookId);
        }
    }
}
