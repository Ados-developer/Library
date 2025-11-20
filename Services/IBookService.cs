using Library.Models;

namespace Library.Services
{
    public interface IBookService
    {
        Task<BookModel?> GetBookByIdAsync(int id);
        Task<List<BookModel>> GetAllBooksAsync();
        Task CreateAsync(BookModel BookModel);
        Task UpdateAsync(BookModel BookModel);
        Task DeleteAsync(int id);
        Task<List<BookModel>> GetAvailableBooksAsync();
        Task<List<BookModel>> GetBorrowedBooksAsync();
    }
}
