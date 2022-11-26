using AxlBookCatalog.Domain.Users;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AxlBookCatalog.DataAccess.Contexts
{
    public class AxlIdentityDbContext : IdentityDbContext<ApplicationUser>
    {
        public AxlIdentityDbContext(DbContextOptions<AxlIdentityDbContext> options) : base(options)
        {
        }
    }
}