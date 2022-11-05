using System.ComponentModel.DataAnnotations;

namespace PJATKInżynierka.DTOs.CyclesDTOs
{
    public class CreateCycleDTO
    {
        [MaxLength(255, ErrorMessage = "String length cannot be longer than 255")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "DateOut is required")]
        public DateTime DateOut { get; set; }

        [Required]
        public int HatcheryOrderID { get; set; }
    }
}
