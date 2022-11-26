using AxlBookCatalog.DataAccess.Abstractions.Repositories;
using AxlBookCatalog.DataAccess.Contexts;
using AxlBookCatalog.Domain;
using Microsoft.EntityFrameworkCore;

namespace AxlBookCatalog.DataAccess.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly AxlBookDbContext _dbContext;

        public BookRepository(AxlBookDbContext axlBookDbContext)
        {
            _dbContext = axlBookDbContext;
        }
        
        public async Task<Book> CreateAsync(Book book)
        {
            var result = (await _dbContext.Books.AddAsync(book)).Entity;
            await _dbContext.SaveChangesAsync();

            return result;
        }

        public async Task<Book?> RemoveAsync(string bookId)
        {
            var result = await GetById(bookId);
            if (result == null)
                return null;

            _dbContext.Books.Remove(result);
            await _dbContext.SaveChangesAsync();

            return result;
        }

        public async Task<Book?> UpdateAsync(Book book)
        {
            var result = await GetById(book.Id);
            if (result == null)
                return null;

            result.AuthorId = book.AuthorId;
            result.Description = book.Description;
            result.Year = book.Year;
            result.Cover = book.Cover;
            result.PageCount = book.PageCount;
            result.Title = book.Title;

            return _dbContext.Books.Update(result).Entity;
        }

        private async Task<Book?> GetById(string id)
        {
            return await _dbContext.Books
                .Where(e=> e.Id == id)
                .FirstOrDefaultAsync();
        }
    }
}
