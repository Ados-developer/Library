namespace Library.Models
{
    public class BorrowBookModel
    {
        public int LoanId { get; set; }
        public int? BookId { get; set; }
        public string? ReaderCardId { get; set; }
        public DateTime LoanDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public List<BookModel>? AvailableBooks { get; set; }
        public List<ReaderModel>? Readers { get; set; }
    }
}
