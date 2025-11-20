using Library.Models;

namespace Library.Services
{
    public interface ILoanService
    {
        Task<List<LoanModel>> GetAllLoansAsync();
        Task<BorrowBookModel> PrepareBorrowBookModelAsync();
        Task BorrowBookAsync(string? readerCardId, int? bookId);
        Task<ReturnBookModel> PrepareReturnBookModelAsync();
        Task ReturnBookAsync(int? bookId);
    }
}
