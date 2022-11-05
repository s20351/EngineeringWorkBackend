using System.ComponentModel.DataAnnotations;

namespace PJATKInżynierka.DTOs.OrderHatcheryDTOs
{
    public class AddOrderHatcheryDTO
    {
        [Required]
        public int HatcheryID{ get; set; }

        [Required]
        public DateTime DateOfArrival { get; set; }

        [Required]
        public int NumberMale { get; set; }

        [Required]
        public int NumberFemale { get; set; }
    }
}
