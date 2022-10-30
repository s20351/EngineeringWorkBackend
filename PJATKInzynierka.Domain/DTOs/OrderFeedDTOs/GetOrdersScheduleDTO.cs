namespace Domain.DTOs.OrderFeedDTOs
{
    public class GetOrdersScheduleDTO
    {
        public string FermName { get; set; }
        public DateTime ArrivalDate { get; set; }
        public decimal Weight { get; set; }
    }
}
