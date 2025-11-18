using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Library.ViewModels
{
    public class LoanViewModel
    {
        public int LoanId { get; set; }
        public int? BookId { get; set; }
        public string? ReaderCardId { get; set; }
        public DateTime LoanDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public List<BookViewModel>? AvailableBooks { get; set; }
        public List<ReaderViewModel>? Readers { get; set; }
    }
}
