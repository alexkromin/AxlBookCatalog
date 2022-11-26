using Microsoft.AspNetCore.Identity;

namespace AxlBookCatalog.Domain.Users
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        /// <summary>
        /// Книжная полка
        /// </summary>
        //public string[] Favourites { get; set; }

        public List<RefreshToken> RefreshTokens { get; set; }
    }
}
