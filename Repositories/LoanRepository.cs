using Library.Data;
using Library.Models;
using Microsoft.EntityFrameworkCore;

namespace Library.Repositories
{
    public class LoanRepository : ILoanRepository
    {
        private readonly LibraryDbContext _context;
        public LoanRepository(LibraryDbContext context)
        {
            _context = context;
        }
        public async Task<List<Loan>> GetAllLoansAsync()
        {
            return await _context.Loans
                .Include(l => l.Book)
                .Include(l => l.Reader)
                .ToListAsync();
        }
        public async Task<Loan?> GetLoanByIdAsync(int id)
        {
            return await _context.Loans.FirstOrDefaultAsync(l => l.Id == id);
        }
        public async Task AddAsync(Loan loan)
        {
            await _context.Loans.AddAsync(loan);
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
        public async Task<Loan?> GetActiveLoanByBookIdAsync(int bookId)
        {
            return await _context.Loans
                .Include(l => l.Book)
                .Include(l => l.Reader)
                .FirstOrDefaultAsync(l => l.BookId == bookId && l.ReturnDate == null);
        }
    }
}
