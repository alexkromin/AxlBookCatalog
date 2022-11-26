namespace AxlBookCatalog.DataAccess.Abstractions.Repositories
{
    public interface IUserRepository
    {
        Task AddToFavouritesAsync(string userId, string bookId);
    }
}