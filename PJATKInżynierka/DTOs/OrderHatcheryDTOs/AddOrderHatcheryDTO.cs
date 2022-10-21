using System.ComponentModel.DataAnnotations;

namespace PJATKInżynierka.DTOs.OrderHatcheryDTOs
{
    public class AddOrderHatcheryDTO
    {
        [MaxLength(255, ErrorMessage = "String length cannot be longer than 255")]
        public string? SupplierName { get; set; }

        [Required]
        public DateTime DateOfOrder { get; set; }

        [Required]
        public DateTime DateOfArrival { get; set; }

        [Required]
        public int NumberMale { get; set; }

        [Required]
        public int NumberFemale { get; set; }

        public decimal Price { get; set; }

    }
}
