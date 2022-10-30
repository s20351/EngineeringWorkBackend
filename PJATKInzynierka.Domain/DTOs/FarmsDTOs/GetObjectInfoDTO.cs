namespace PJATKInżynierka.DTOs.FarmsDTOs
{
    public class GetObjectInfoDTO
    {
        public int ObjectID { get; set; }
        public string ObjectName { get; set; }
        public int? AliveMale { get; set; }
        public int? AliveFemale { get; set; }
        public int DeadMale { get; set; }
        public int DeadFemale { get; set; }
        public int BreedingDay {get; set;}
        public int DaysToExport { get; set; }
    }
}
