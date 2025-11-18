namespace Library.ViewModels
{
    public class AllLoansViewModel
    {
        public int LoanId { get; set; }
        public ReaderViewModel? Reader { get; set;}
        public BookViewModel? Book { get; set; }
        public DateTime LoanDate { get; set; }
        public DateTime? ReturnDate { get; set; }
    }
}
