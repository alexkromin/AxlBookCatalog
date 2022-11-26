using AxlBookCatalog.DataAccess.Abstractions.Repositories;
using AxlBookCatalog.Domain.Users;
using Microsoft.AspNetCore.Identity;

namespace AxlBookCatalog.DataAccess.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserRepository(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task AddToFavouritesAsync(string userId, string bookId)
        {
            throw new Exception();
        }
    }
}
