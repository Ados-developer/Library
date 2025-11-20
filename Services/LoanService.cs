using Library.Models;
using Library.Repositories;
using Library.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Library.Services
{
    public class LoanService : ILoanService
    {
        private readonly ILoanRepository _loanRepository;
        private readonly IBookService _bookService;
        private readonly IBookRepository _bookRepository;
        private readonly IReaderService _readerService;
        public LoanService(ILoanRepository loanRepository, IBookService bookService, IBookRepository bookRepository, IReaderService readerService)
        {
            _loanRepository = loanRepository;
            _bookService = bookService;
            _bookRepository = bookRepository;
            _readerService = readerService;
        }
        public async Task<List<LoanModel>> GetAllLoansAsync()
        {
            List<Loan> loans = await _loanRepository.GetAllLoansAsync();

            List<LoanModel> loansViewModels = loans.Select(l => new LoanModel
            {
                LoanId = l.Id,
                LoanDate = l.LoanDate,
                ReturnDate = l.ReturnDate,
                Reader = new ReaderModel
                {
                    FirstName = l.Reader.FirstName,
                    LastName = l.Reader.LastName
                },
                Book = new BookModel
                {
                    Title = l.Book.Title,
                }
            }).ToList();

            return loansViewModels;
        }
        public async Task<BorrowBookModel> PrepareBorrowBookModelAsync()
        {
            List<ReaderModel> readers = await _readerService.GetAllReadersAsync();
            List<BookModel> books = await _bookService.GetAvailableBooksAsync();
            BorrowBookModel viewModel = new BorrowBookModel
            {
                AvailableBooks = books,
                Readers = readers
            };
            return viewModel;
        }
        public async Task BorrowBookAsync(string? readerCardId, int? bookId)
        {
            if (bookId == null)
                throw new Exception("Musíte vybrať knihu.");

            if (string.IsNullOrWhiteSpace(readerCardId))
                throw new Exception("Musíte vybrať čitateľa.");

            Book? book = await _bookRepository.GetBookByIdAsync(bookId.Value);
            if (book == null || book.IsBorrowed)
                throw new Exception("Kniha nie je dostupná.");

            ReaderModel? reader = await _readerService.GetReaderByIdAsync(readerCardId);
            if (reader == null)
                throw new Exception("Čitateľ neexistuje.");

            Loan? loan = new Loan
            {
                BookId = book.Id,
                ReaderCardId = reader.CardId,
                LoanDate = DateTime.Now
            };
            await _loanRepository.AddAsync(loan);
            book.IsBorrowed = true;

            await _loanRepository.SaveChangesAsync();
            await _bookRepository.SaveChangesAsync();
        }
        public async Task<ReturnBookModel> PrepareReturnBookModelAsync()
        {
            List<BookModel> books = await _bookService.GetBorrowedBooksAsync();
            ReturnBookModel viewModel = new ReturnBookModel
            {
                BorrowedBook = books
            };
            return viewModel;
        }
        public async Task ReturnBookAsync(int? bookId)
        {
            if(bookId == null)
                throw new Exception("Musíte vybrať knihu.");
            Loan? loan = await _loanRepository.GetActiveLoanByBookIdAsync(bookId.Value);
            if (loan == null)
                throw new Exception("Kniha nie je aktuálne požičaná");

            loan.ReturnDate = DateTime.Now;

            Book? book = await _bookRepository.GetBookByIdAsync(bookId.Value);
            if(book != null)
            {
                book.IsBorrowed = false;
            }

            await _bookRepository.SaveChangesAsync();
            await _loanRepository.SaveChangesAsync();
        }
    }
}
