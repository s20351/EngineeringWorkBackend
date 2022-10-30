using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public partial class OrderFeed
    {
        public int OrderFeedId { get; set; }
        public DateTime DateOfOrder { get; set; }
        public DateTime DateOfArrival { get; set; }
        public decimal Weight { get; set; }
        public int FeedhouseFeedhouseId { get; set; }
        public int? FarmFarmId { get; set; }

        public virtual Farm? FarmFarm { get; set; }
        public virtual Feedhouse FeedhouseFeedhouse { get; set; } = null!;
    }
}
