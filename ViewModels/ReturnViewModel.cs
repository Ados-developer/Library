
namespace Library.ViewModels
{
    public class ReturnViewModel
    {
        public int? BookId {  get; set; }
        public List<BookViewModel>? BorrowedBook { get; set; }
    }
}
