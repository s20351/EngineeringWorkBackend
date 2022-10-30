using System.ComponentModel.DataAnnotations;

namespace PJATKInżynierka.DTOs.FarmersDTOs
{
    public class AddFarmerDTO
    {
        [Required(ErrorMessage = "Name is required")]
        [MaxLength(255, ErrorMessage = "String length cannot be longer than 255")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Surname is required")]
        [MaxLength(255, ErrorMessage = "String length cannot be longer than 255")]
        public string? Surname { get; set; }

        [MaxLength(255, ErrorMessage = "String length cannot be longer than 255")]
        public string? FarmerColor { get; set; }
    }
}
