using System;
using System.Collections.Generic;

namespace PJATKInżynierka.Models
{
    public partial class OrderFeed
    {
        public int OrderFeedId { get; set; }
        public string? SupplierName { get; set; }
        public DateTime? DateOfOrder { get; set; }
        public DateTime? DateOfArrival { get; set; }
        public decimal? Weight { get; set; }
        public decimal? Price { get; set; }
        public int? FarmId { get; set; }

        public virtual Farm? Farm { get; set; }
    }
}
