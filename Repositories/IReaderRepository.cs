using Library.Models;

namespace Library.Repositories
{
    public interface IReaderRepository
    {
        Task<Reader?> GetReaderByIdAsync(string id);
        Task<List<Reader>> GetAllReadersAsync();
        Task AddAsync(Reader reader);
        void Update(Reader reader);
        Task DeleteAsync(string id);
        Task SaveChangesAsync();
    }
}
