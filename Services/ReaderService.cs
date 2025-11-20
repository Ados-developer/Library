using Library.Entities;
using Library.Models;
using Library.Repositories;


namespace Library.Services
{
    public class ReaderService : IReaderService
    {
        private readonly IReaderRepository _readerRepo;
        public ReaderService(IReaderRepository readerRepo)
        {
            _readerRepo = readerRepo;
        }
        public async Task<ReaderModel?> GetReaderByIdAsync(string id)
        {
            Reader? reader = await _readerRepo.GetReaderByIdAsync(id);
            if (reader == null)
            {
                return null;
            }
            ReaderModel viewModel = new ReaderModel
            {
                CardId = reader.CardId,
                FirstName = reader.FirstName,
                LastName = reader.LastName,
                DateOfBirth = reader.DateOfBirth,
            };
            return viewModel;
        }
        public async Task<List<ReaderModel>> GetAllReadersAsync()
        {
            List<Reader> readers = await _readerRepo.GetAllReadersAsync();
            return readers.Select(reader => new ReaderModel
            {
                CardId = reader.CardId,
                FirstName = reader.FirstName,
                LastName = reader.LastName,
                DateOfBirth = reader.DateOfBirth
            }).ToList();
        }
        public async Task CreateAsync(ReaderModel ReaderModel)
        {
            Reader? reader = new Reader
            {
                CardId = ReaderModel.CardId,
                FirstName = ReaderModel.FirstName,
                LastName = ReaderModel.LastName,
                DateOfBirth = ReaderModel.DateOfBirth
            };
            await _readerRepo.AddAsync(reader);
            await _readerRepo.SaveChangesAsync();
        }
        public async Task UpdateAsync(ReaderModel ReaderModel)
        {
            Reader? reader = new Reader
            {
                CardId = ReaderModel.CardId,
                FirstName = ReaderModel.FirstName,
                LastName = ReaderModel.LastName,
                DateOfBirth = ReaderModel.DateOfBirth
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
