using Library.Services;
using Library.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers
{
    public class ReaderController : Controller
    {
        private readonly IReaderService _readerService;
        public ReaderController(IReaderService readerService)
        {
            _readerService = readerService;
        }
        public async Task<IActionResult> GetRecords()
        {
            List<ReaderViewModel> viewModels = await _readerService.GetAllReadersAsync();
            return View(viewModels);
        }
        public IActionResult AddRecord()
        {
            ReaderViewModel readerViewModel = new ReaderViewModel();
            return View(readerViewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddRecord(ReaderViewModel readerViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(readerViewModel);
            }
            await _readerService.CreateAsync(readerViewModel);
            TempData["Success"] = "Reader was added.";
            return RedirectToAction("GetRecords");
        }
        public async Task<IActionResult> EditRecord(string id)
        {
            ReaderViewModel? readerViewModel = await _readerService.GetReaderByIdAsync(id);
            if (readerViewModel == null)
                return NotFound();
            return View(readerViewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditRecord(ReaderViewModel readerViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(readerViewModel);
            }
            await _readerService.UpdateAsync(readerViewModel);
            TempData["Success"] = "Reader was edited.";
            return RedirectToAction("GetRecords");
        }
        public async Task<IActionResult> DeleteRecord(string id)
        {
            ReaderViewModel? readerViewModel = await _readerService.GetReaderByIdAsync(id);
            if (readerViewModel == null)
                return NotFound();
            return View(readerViewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmDelete(string id)
        {
            await _readerService.DeleteAsync(id);
            TempData["Success"] = "Reader was removed succesfully.";
            return RedirectToAction("GetRecords");
        }
    }
}
