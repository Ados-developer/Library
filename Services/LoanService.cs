using Library.Models;
using Library.Repositories;
using Library.ViewModels;
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
        public async Task<List<AllLoansViewModel>> GetAllLoansAsync()
        {
            List<Loan> loans = await _loanRepository.GetAllLoansAsync();

            List<AllLoansViewModel> loansViewModels = loans.Select(l => new AllLoansViewModel
            {
                LoanId = l.Id,
                LoanDate = l.LoanDate,
                ReturnDate = l.ReturnDate,
                Reader = new ReaderViewModel
                {
                    FirstName = l.Reader.FirstName,
                    LastName = l.Reader.LastName
                },
                Book = new BookViewModel
                {
                    Title = l.Book.Title,
                }
            }).ToList();

            return loansViewModels;
        }
        public async Task<LoanViewModel> PrepareLoanViewModelAsync()
        {
            List<ReaderViewModel> readers = await _readerService.GetAllReadersAsync();
            List<BookViewModel> books = await _bookService.GetAvailableBooksAsync();
            LoanViewModel viewModel = new LoanViewModel
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

            ReaderViewModel? reader = await _readerService.GetReaderByIdAsync(readerCardId);
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
        public async Task<ReturnViewModel> PrepareReturnViewModelAsync()
        {
            List<BookViewModel> books = await _bookService.GetBorrowedBooksAsync();
            ReturnViewModel viewModel = new ReturnViewModel
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
