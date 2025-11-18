using System.ComponentModel.DataAnnotations;

namespace Library.ViewModels
{
    public class BookViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Title of Book is required.")]
        [Display(Name = "Title of Book")]
        public string Title { get; set; } = string.Empty;
        [Required(ErrorMessage = "Author is required")]
        [Display(Name = "Author of Book")]
        public string Author { get; set; } = string.Empty;
        public bool IsBorrowed { get; set; }
        public string? BorrowedBy { get; set; }
    }
}
