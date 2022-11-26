using AxlBookCatalog.Domain;

namespace AxlBookCatalog.DataAccess.Abstractions.Repositories
{
    public interface IBookRepository
    {
        Task<Book> CreateAsync(Book book);

        Task<Book?> RemoveAsync(string bookId);

        Task<Book?> UpdateAsync(Book book);
    }
}
