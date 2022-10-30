using System.ComponentModel.DataAnnotations;

namespace PJATKInżynierka.DTOs.FarmsDTOs
{
    public class AddFarmDTO
    {
        [Required(ErrorMessage = "Name is required")]
        [MaxLength(255, ErrorMessage = "String length cannot be longer than 255")]
        public string? Name { get; set; }

        public int? AddressID { get; set; }

        [MaxLength(255, ErrorMessage = "String length cannot be longer than 255")]
        public string? FarmColor { get; set; }
    }
}
