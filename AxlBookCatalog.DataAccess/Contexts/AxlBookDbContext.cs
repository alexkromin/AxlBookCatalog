using AxlBookCatalog.Domain;
using Microsoft.EntityFrameworkCore;

namespace AxlBookCatalog.DataAccess.Contexts
{
    public class AxlBookDbContext : DbContext
    {
        public DbSet<Book> Books { get; set;}

        public AxlBookDbContext()
        {
            Database.EnsureCreated();
        }

    }
}
