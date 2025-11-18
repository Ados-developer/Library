using System.ComponentModel.DataAnnotations;

namespace Library.ViewModels
{
    public class ReaderViewModel
    {
        [Required(ErrorMessage = "Number of ID card is required")]
        [Display(Name = "Number of ID card")]
        public string CardId { get; set; } = string.Empty;
        [Required(ErrorMessage = "Name is required")]
        [Display(Name = "Name")]
        public string FirstName { get; set; } = string.Empty;
        [Required(ErrorMessage = "Surname is required")]
        [Display(Name = "Surname")]
        public string LastName { get; set; } = string.Empty;
        [Required(ErrorMessage = "Date of birth is required")]
        [Display(Name = "Date of birth")]
        public DateTime DateOfBirth { get; set; }
    }
}
