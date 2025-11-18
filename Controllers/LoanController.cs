using Library.Services;
using Library.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers
{
    public class LoanController : Controller
    {
        private readonly ILoanService _loanService;
        private readonly IBookService _bookService;
        private readonly IReaderService _readerService;
        public LoanController(ILoanService loanService, IBookService bookService, IReaderService readerService)
        {
            _loanService = loanService;
            _bookService = bookService;
            _readerService = readerService;
        }
        public async Task<IActionResult> GetRecords()
        {
            List<AllLoansViewModel> loans = await _loanService.GetAllLoansAsync();
            return View(loans);
        }
        public async Task<IActionResult> CreateLoan()
        {
            LoanViewModel model = await _loanService.PrepareLoanViewModelAsync();
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateLoan(LoanViewModel model)
        {
            if (!ModelState.IsValid)
            {
                LoanViewModel viewModel = await _loanService.PrepareLoanViewModelAsync();
                return View(viewModel);
            }
            try
            {
                await _loanService.BorrowBookAsync(model.ReaderCardId, model.BookId);
                TempData["Success"] = "Book was borrowed.";
                return RedirectToAction("CreateLoan");
            } catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                LoanViewModel viewModel = await _loanService.PrepareLoanViewModelAsync();
                return View(viewModel);
            }
        }
        public async Task<IActionResult> ReturnBook()
        {
            ReturnViewModel model = await _loanService.PrepareReturnViewModelAsync();
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ReturnBook(ReturnViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ReturnViewModel viewModel = await _loanService.PrepareReturnViewModelAsync();
                return View(viewModel);
            }
            try
            {
                await _loanService.ReturnBookAsync(model.BookId);
                TempData["Success"] = "Book was returned.";
                return RedirectToAction("ReturnBook");
            } catch(Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                ReturnViewModel viewModel = await _loanService.PrepareReturnViewModelAsync();
                return View(viewModel);
            }
        }
    }
}
