using Library.Entities;

namespace Library.Repositories
{
    public interface ILoanRepository
    {
        Task<List<Loan>> GetAllLoansAsync();
        Task<Loan?> GetLoanByIdAsync(int id);
        Task AddAsync(Loan loan);
        Task SaveChangesAsync();
        Task<Loan?> GetActiveLoanByBookIdAsync(int bookId);
    }
}
