using System.ComponentModel.DataAnnotations;

namespace PJATKInżynierka.DTOs.OrderFeedDTOs
{
    public class AddOrderFeedDTO
    {
        [MaxLength(255, ErrorMessage = "String length cannot be longer than 255")]
        public string? SupplierName { get; set; }
        [Required]
        public DateTime DateOfOrder { get ; set; }
        [Required]
        public DateTime DateOfArrival { get; set; }
        public decimal Weight { get; set; }
        public decimal Price { get; set; }
    }
}
