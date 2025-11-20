using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Entities
{
    public class Loan
    {
        public int Id { get; set; }
        public DateTime LoanDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        [ForeignKey(nameof(Reader))]
        public string ReaderCardId { get; set; } = string.Empty;
        public Reader Reader { get; set; } = null!;
        [ForeignKey(nameof(Book))]
        public int BookId { get; set; }
        public Book Book { get; set; } = null!;
    }
}
