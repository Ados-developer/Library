using Library.Models;

namespace Library.Services
{
    public interface IReaderService
    {
        Task<ReaderModel?> GetReaderByIdAsync(string id);
        Task<List<ReaderModel>> GetAllReadersAsync();
        Task CreateAsync(ReaderModel readerViewModel);
        Task UpdateAsync(ReaderModel readerViewModel);
        Task DeleteAsync(string id);
    }
}
