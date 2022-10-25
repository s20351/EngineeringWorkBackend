using System.ComponentModel.DataAnnotations;

namespace PJATKInżynierka.DTOs.FarmsDTOs
{
    public class AddFarmDTO
    {
        [Required(ErrorMessage = "Name is required")]
        [MaxLength(255, ErrorMessage = "String length cannot be longer than 255")]
        public string? Name { get; set; }

        [MaxLength(255, ErrorMessage = "String length cannot be longer than 255")]
        public string? Address { get; set; }

        [MaxLength(255, ErrorMessage = "String length cannot be longer than 255")]
        public string? FarmColor { get; set; }
    }
}
