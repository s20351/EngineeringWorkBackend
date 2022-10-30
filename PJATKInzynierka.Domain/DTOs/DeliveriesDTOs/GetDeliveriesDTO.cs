namespace Domain.DTOs.DeliveriesDTOs
{
    public class GetDeliveriesDTO
    {
        public DateTime Date { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public decimal Weight { get; set; }
    }
}
