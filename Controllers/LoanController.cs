using Library.Models;
using Library.Services;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers
{
    public class LoanController : Controller
    {
        private readonly ILoanService _loanService;
        public LoanController(ILoanService loanService)
        {
            _loanService = loanService;
        }
        public async Task<IActionResult> GetRecords()
        {
            List<LoanModel> loans = await _loanService.GetAllLoansAsync();
            return View(loans);
        }
        public async Task<IActionResult> CreateLoan()
        {
            BorrowBookModel model = await _loanService.PrepareBorrowBookModelAsync();
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateLoan(BorrowBookModel model)
        {
            if (!ModelState.IsValid)
            {
                BorrowBookModel viewModel = await _loanService.PrepareBorrowBookModelAsync();
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
                BorrowBookModel viewModel = await _loanService.PrepareBorrowBookModelAsync();
                return View(viewModel);
            }
        }
        public async Task<IActionResult> ReturnBook()
        {
            ReturnBookModel model = await _loanService.PrepareReturnBookModelAsync();
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ReturnBook(ReturnBookModel model)
        {
            if (!ModelState.IsValid)
            {
                ReturnBookModel viewModel = await _loanService.PrepareReturnBookModelAsync();
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
                ReturnBookModel viewModel = await _loanService.PrepareReturnBookModelAsync();
                return View(viewModel);
            }
        }
    }
}
