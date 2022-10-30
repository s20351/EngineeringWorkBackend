using System.ComponentModel.DataAnnotations;

namespace Domain.DTOs.DeliveriesDTOs
{
    public class AddDeliveryDTO
    {
        [Required]
        public DateTime DeliveryDate { get; set; }
        public decimal Weight { get; set; }
    }
}
