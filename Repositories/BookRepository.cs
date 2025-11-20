using Library.Data;
using Library.Entities;
using Microsoft.EntityFrameworkCore;

namespace Library.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly LibraryDbContext _context;
        public BookRepository(LibraryDbContext context)
        {
            _context = context;
        }
        public async Task<Book?> GetBookByIdAsync(int id)
        {
            return await _context.Books.FirstOrDefaultAsync(b => b.Id == id);
        }
        public async Task<List<Book>> GetAllBooksAsync()
        {
            return await _context.Books.ToListAsync();
        }
        public async Task AddAsync(Book book)
        {
            await _context.Books.AddAsync(book);
        }
        public void Update(Book book)
        {
            _context.Books.Update(book);
        }
        public async Task DeleteAsync(int id)
        {
            Book? book = await _context.Books.FirstOrDefaultAsync(b => b.Id==id);
            if (book != null)
            {
                _context.Books.Remove(book);
            }
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
        public async Task<List<Book>> GetAvailableBooksAsync()
        {
            return await _context.Books
                .Where(b => !b.IsBorrowed)
                .ToListAsync();
        }
        public async Task<List<Book>> GetBorrowedBooksAsync()
        {
            return await _context.Books
                .Where(b =>  b.IsBorrowed)
                .ToListAsync();
        }
    }
}
