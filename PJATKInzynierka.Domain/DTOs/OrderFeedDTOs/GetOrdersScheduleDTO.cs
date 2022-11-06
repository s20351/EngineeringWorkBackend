namespace Domain.DTOs.OrderFeedDTOs
{
    public class GetOrdersScheduleDTO
    {
        public int ObjectID { get; set; }
        public string? FarmName { get; set; }
        public string ArrivalDate { get; set; }
        public decimal Weight { get; set; }
    }
}
