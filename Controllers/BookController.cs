using Library.Models;
using Library.Services;
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
            List<BookModel> viewModels = await _bookService.GetAllBooksAsync();
            return View(viewModels);
        }
        public IActionResult AddRecord()
        {
            BookModel BookModel = new BookModel();
            return View(BookModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddRecord(BookModel BookModel)
        {
            if (!ModelState.IsValid)
            {
                return View(BookModel);
            }
            await _bookService.CreateAsync(BookModel);
            TempData["Success"] = "Book was added.";
            return RedirectToAction("GetRecords");
        }
        public async Task<IActionResult> EditRecord(int id)
        {
            BookModel? BookModel = await _bookService.GetBookByIdAsync(id);
            if (BookModel == null)
                return NotFound();
            return View(BookModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditRecord(BookModel BookModel)
        {
            if (!ModelState.IsValid)
            {
                return View(BookModel);
            }
            await _bookService.UpdateAsync(BookModel);
            TempData["Success"] = "Book was edited.";
            return RedirectToAction("GetRecords");
        }
        public async Task<IActionResult> DeleteRecord(int id)
        {
            BookModel? BookModel = await _bookService.GetBookByIdAsync(id);
            if (BookModel == null)
                return NotFound();
            return View(BookModel);
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
