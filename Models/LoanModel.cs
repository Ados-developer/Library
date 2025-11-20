namespace Library.Models
{
    public class LoanModel
    {
        public int LoanId { get; set; }
        public ReaderModel? Reader { get; set; }
        public BookModel? Book { get; set; }
        public DateTime LoanDate { get; set; }
        public DateTime? ReturnDate { get; set; }
    }
}
