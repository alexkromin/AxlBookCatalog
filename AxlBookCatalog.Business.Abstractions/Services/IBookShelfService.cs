namespace AxlBookCatalog.Business.Abstractions.Services
{
    public interface IBookShelfService
    {
        Task AddAsync(string bookId);
    }
}
