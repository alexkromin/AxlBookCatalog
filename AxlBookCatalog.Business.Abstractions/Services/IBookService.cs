using AxlBookCatalog.Business.Models;
using AxlBookCatalog.Business.Models.Book;
using AxlBookCatalog.Domain;

namespace AxlBookCatalog.Business.Abstractions.Services
{
    public interface IBookService
    {
        Task<IEnumerable<Book>> GetByStringAsync(string str);

        Task<CommandOperationResponse<Book>> AddAsync(AddBookRequest request);

        Task<CommandOperationResponse<Book>> EditAsync(EditBookRequest request);

        Task<CommandOperationResponse<Book>> RemoveAsync(string bookId);
    }
}
