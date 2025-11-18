using Library.ViewModels;

namespace Library.Services
{
    public interface ILoanService
    {
        Task<List<AllLoansViewModel>> GetAllLoansAsync();
        Task<LoanViewModel> PrepareLoanViewModelAsync();
        Task BorrowBookAsync(string? readerCardId, int? bookId);
        Task<ReturnViewModel> PrepareReturnViewModelAsync();
        Task ReturnBookAsync(int? bookId);
    }
}
