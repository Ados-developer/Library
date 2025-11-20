using Library.Entities;
using Library.Repositories;
using Library.Models;

namespace Library.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepo;
        private readonly ILoanRepository _loanRepository;
        private readonly IReaderRepository _readerRepository;
        public BookService(IBookRepository bookRepo, ILoanRepository loanRepository, IReaderRepository readerRepository)
        {
            _bookRepo = bookRepo;
            _loanRepository = loanRepository;
            _readerRepository = readerRepository;
        }
        public async Task<BookModel?> GetBookByIdAsync(int id)
        {
            Book? book = await _bookRepo.GetBookByIdAsync(id);
            if(book == null)
            {
                return null;
            }
            BookModel viewModel = new BookModel
            {
                Id = book.Id,
                Title = book.Title,
                Author = book.Author,
                IsBorrowed = book.IsBorrowed,
            };
            return viewModel;
        }
        public async Task<List<BookModel>> GetAllBooksAsync()
        {
            List<Book> books = await _bookRepo.GetAllBooksAsync();
            List<BookModel> viewModels = new List<BookModel>();
            foreach(Book book in books)
            {
                string borrowedBy = "";
                if (book.IsBorrowed)
                {
                    var loan = await _loanRepository.GetActiveLoanByBookIdAsync(book.Id);
                    if (loan != null)
                    {
                        var reader = await _readerRepository.GetReaderByIdAsync(loan.ReaderCardId);
                        borrowedBy = reader != null ? $"{reader.FirstName} {reader.LastName}" : "";
                    }
                }
                viewModels.Add(new BookModel
                {
                    Id = book.Id,
                    Title = book.Title,
                    Author = book.Author,
                    IsBorrowed = book.IsBorrowed,
                    BorrowedBy = borrowedBy
                });
            }
            return viewModels;
        }
        public async Task CreateAsync(BookModel BookModel)
        {
            Book? book = new Book
            {
                Id = BookModel.Id,
                Title = BookModel.Title,
                Author = BookModel.Author,
                IsBorrowed = false,
            };
            await _bookRepo.AddAsync(book);
            await _bookRepo.SaveChangesAsync();
        }
        public async Task UpdateAsync(BookModel BookModel)
        {
            Book? book = new Book
            {
                Id = BookModel.Id,
                Title = BookModel.Title,
                Author = BookModel.Author,
                IsBorrowed = BookModel.IsBorrowed
            };
            _bookRepo.Update(book);
            await _bookRepo.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            await _bookRepo.DeleteAsync(id);
            await _bookRepo.SaveChangesAsync();
        }
        public async Task<List<BookModel>> GetAvailableBooksAsync()
        {
            List<Book> books = await _bookRepo.GetAvailableBooksAsync();
            return books.Select(book => new BookModel
            {
                Id = book.Id,
                Title = book.Title,
                Author = book.Author,
                IsBorrowed= book.IsBorrowed,
            }).ToList();
        }
        public async Task<List<BookModel>> GetBorrowedBooksAsync()
        {
            List<Book> books = await _bookRepo.GetBorrowedBooksAsync();
            return books.Select(book => new BookModel
            {
                Id = book.Id,
                Title = book.Title,
                Author = book.Author,
                IsBorrowed = book.IsBorrowed,
            }).ToList();
        }
    }
}
