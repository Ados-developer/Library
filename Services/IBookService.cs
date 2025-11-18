using Library.ViewModels;

namespace Library.Services
{
    public interface IBookService
    {
        Task<BookViewModel?> GetBookByIdAsync(int id);
        Task<List<BookViewModel>> GetAllBooksAsync();
        Task CreateAsync(BookViewModel bookViewModel);
        Task UpdateAsync(BookViewModel bookViewModel);
        Task DeleteAsync(int id);
        Task<List<BookViewModel>> GetAvailableBooksAsync();
        Task<List<BookViewModel>> GetBorrowedBooksAsync();
    }
}
