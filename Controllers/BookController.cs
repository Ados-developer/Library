using Library.Services;
using Library.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookService _bookService;
        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }
        public async Task<IActionResult> GetRecords()
        {
            List<BookViewModel> viewModels = await _bookService.GetAllBooksAsync();
            return View(viewModels);
        }
        public IActionResult AddRecord()
        {
            BookViewModel bookViewModel = new BookViewModel();
            return View(bookViewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddRecord(BookViewModel bookViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(bookViewModel);
            }
            await _bookService.CreateAsync(bookViewModel);
            TempData["Success"] = "Book was added.";
            return RedirectToAction("GetRecords");
        }
        public async Task<IActionResult> EditRecord(int id)
        {
            BookViewModel? bookViewModel = await _bookService.GetBookByIdAsync(id);
            if (bookViewModel == null)
                return NotFound();
            return View(bookViewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditRecord(BookViewModel bookViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(bookViewModel);
            }
            await _bookService.UpdateAsync(bookViewModel);
            TempData["Success"] = "Book was edited.";
            return RedirectToAction("GetRecords");
        }
        public async Task<IActionResult> DeleteRecord(int id)
        {
            BookViewModel? bookViewModel = await _bookService.GetBookByIdAsync(id);
            if (bookViewModel == null)
                return NotFound();
            return View(bookViewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmDelete(int id)
        {
            await _bookService.DeleteAsync(id);
            TempData["Success"] = "Book was removed succesfully.";
            return RedirectToAction("GetRecords");
        }
    }
}
