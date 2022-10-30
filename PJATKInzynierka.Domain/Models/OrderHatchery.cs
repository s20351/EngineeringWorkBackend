using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public partial class OrderHatchery
    {
        public int OrderHatcheryId { get; set; }
        public DateTime DataOfOrder { get; set; }
        public DateTime DataOfArrival { get; set; }
        public int NumberMale { get; set; }
        public int NumberFemale { get; set; }
        public int HatcheryHatcheryId { get; set; }
        public int FarmFarmId { get; set; }

        public virtual Farm FarmFarm { get; set; } = null!;
        public virtual Hatchery HatcheryHatchery { get; set; } = null!;
    }
}
