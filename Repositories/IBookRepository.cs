using Library.Entities;

namespace Library.Repositories
{
    public interface IBookRepository
    {
        Task<Book?> GetBookByIdAsync(int id);
        Task<List<Book>> GetAllBooksAsync();
        Task AddAsync(Book book);
        void Update(Book book);
        Task DeleteAsync(int id);
        Task SaveChangesAsync();
        Task<List<Book>> GetAvailableBooksAsync();
        Task<List<Book>> GetBorrowedBooksAsync();
    }
}
