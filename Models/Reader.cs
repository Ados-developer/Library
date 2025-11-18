using System.ComponentModel.DataAnnotations;

namespace Library.Models
{
    public class Reader
    {
        [Key]
        public string CardId { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }

    }
}
