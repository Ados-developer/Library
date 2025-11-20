namespace Library.Models
{
    public class ReturnBookModel
    {
        public int? BookId { get; set; }
        public List<BookModel>? BorrowedBook { get; set; }
    }
}
