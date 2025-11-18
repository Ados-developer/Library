using Library.Models;
using Library.Repositories;
using Library.ViewModels;

namespace Library.Services
{
    public class ReaderService : IReaderService
    {
        private readonly IReaderRepository _readerRepo;
        public ReaderService(IReaderRepository readerRepo)
        {
            _readerRepo = readerRepo;
        }
        public async Task<ReaderViewModel?> GetReaderByIdAsync(string id)
        {
            Reader? reader = await _readerRepo.GetReaderByIdAsync(id);
            if (reader == null)
            {
                return null;
            }
            ReaderViewModel viewModel = new ReaderViewModel
            {
                CardId = reader.CardId,
                FirstName = reader.FirstName,
                LastName = reader.LastName,
                DateOfBirth = reader.DateOfBirth,
            };
            return viewModel;
        }
        public async Task<List<ReaderViewModel>> GetAllReadersAsync()
        {
            List<Reader> readers = await _readerRepo.GetAllReadersAsync();
            return readers.Select(reader => new ReaderViewModel
            {
                CardId = reader.CardId,
                FirstName = reader.FirstName,
                LastName = reader.LastName,
                DateOfBirth = reader.DateOfBirth
            }).ToList();
        }
        public async Task CreateAsync(ReaderViewModel readerViewModel)
        {
            Reader? reader = new Reader
            {
                CardId = readerViewModel.CardId,
                FirstName = readerViewModel.FirstName,
                LastName = readerViewModel.LastName,
                DateOfBirth = readerViewModel.DateOfBirth
            };
            await _readerRepo.AddAsync(reader);
            await _readerRepo.SaveChangesAsync();
        }
        public async Task UpdateAsync(ReaderViewModel readerViewModel)
        {
            Reader? reader = new Reader
            {
                CardId = readerViewModel.CardId,
                FirstName = readerViewModel.FirstName,
                LastName = readerViewModel.LastName,
                DateOfBirth = readerViewModel.DateOfBirth
            };
            _readerRepo.Update(reader);
            await _readerRepo.SaveChangesAsync();
        }
        public async Task DeleteAsync(string id)
        {
            await _readerRepo.DeleteAsync(id);
            await _readerRepo.SaveChangesAsync();
        }
    }
}
