using System;
using System.Collections.Generic;

namespace PJATKInżynierka.Models
{
    public partial class OrderHatchery
    {
        public int OrderHatcheryId { get; set; }
        public string? SupplierName { get; set; }
        public DateTime DateOfOrder { get; set; }
        public DateTime DateOfArrival { get; set; }
        public int NumberMale { get; set; }
        public int NumberFemale { get; set; }
        public decimal? Price { get; set; }
        public int? FarmId { get; set; }

        public virtual Farm? Farm { get; set; }
    }
}
