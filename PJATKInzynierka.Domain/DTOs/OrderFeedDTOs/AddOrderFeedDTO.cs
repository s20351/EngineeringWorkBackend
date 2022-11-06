using System.ComponentModel.DataAnnotations;

namespace PJATKInżynierka.DTOs.OrderFeedDTOs
{
    public class AddOrderFeedDTO
    {
        [Required]
        public int FeedHouseID { get; set; }
        [Required]
        public DateTime DateOfArrival { get; set; }
        public decimal Weight { get; set; }
    }
}
