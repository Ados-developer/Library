using Library.Data;
using Library.Models;
using Microsoft.EntityFrameworkCore;

namespace Library.Repositories
{
    public class ReaderRepository : IReaderRepository
    {
        private readonly LibraryDbContext _context;
        public ReaderRepository(LibraryDbContext context)
        {
            _context = context;
        }
        public async Task<Reader?> GetReaderByIdAsync(string id)
        {
            return await _context.Readers.FirstOrDefaultAsync(r => r.CardId == id);
        }
        public async Task<List<Reader>> GetAllReadersAsync()
        {
            return await _context.Readers.ToListAsync();
        }
        public async Task AddAsync(Reader reader)
        {
            await _context.Readers.AddAsync(reader);
        }
        public void Update(Reader reader)
        {
            _context.Readers.Update(reader);
        }
        public async Task DeleteAsync(string id)
        {
            Reader? reader = await _context.Readers.FirstOrDefaultAsync(r => r.CardId == id);
            if (reader != null)
            {
                _context.Readers.Remove(reader);
            }
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
