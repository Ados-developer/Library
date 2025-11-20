using Library.Models;
using Library.Services;
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
            List<ReaderModel> viewModels = await _readerService.GetAllReadersAsync();
            return View(viewModels);
        }
        public IActionResult AddRecord()
        {
            ReaderModel ReaderModel = new ReaderModel();
            return View(ReaderModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddRecord(ReaderModel ReaderModel)
        {
            if (!ModelState.IsValid)
            {
                return View(ReaderModel);
            }
            await _readerService.CreateAsync(ReaderModel);
            TempData["Success"] = "Reader was added.";
            return RedirectToAction("GetRecords");
        }
        public async Task<IActionResult> EditRecord(string id)
        {
            ReaderModel? ReaderModel = await _readerService.GetReaderByIdAsync(id);
            if (ReaderModel == null)
                return NotFound();
            return View(ReaderModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditRecord(ReaderModel ReaderModel)
        {
            if (!ModelState.IsValid)
            {
                return View(ReaderModel);
            }
            await _readerService.UpdateAsync(ReaderModel);
            TempData["Success"] = "Reader was edited.";
            return RedirectToAction("GetRecords");
        }
        public async Task<IActionResult> DeleteRecord(string id)
        {
            ReaderModel? ReaderModel = await _readerService.GetReaderByIdAsync(id);
            if (ReaderModel == null)
                return NotFound();
            return View(ReaderModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmDelete(string cardId)
        {
            await _readerService.DeleteAsync(cardId);
            TempData["Success"] = "Reader was removed succesfully.";
            return RedirectToAction("GetRecords");
        }
    }
}
