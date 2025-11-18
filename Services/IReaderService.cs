using Library.ViewModels;

namespace Library.Services
{
    public interface IReaderService
    {
        Task<ReaderViewModel?> GetReaderByIdAsync(string id);
        Task<List<ReaderViewModel>> GetAllReadersAsync();
        Task CreateAsync(ReaderViewModel readerViewModel);
        Task UpdateAsync(ReaderViewModel readerViewModel);
        Task DeleteAsync(string id);
    }
}
