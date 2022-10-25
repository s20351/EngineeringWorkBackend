using System;
using System.Collections.Generic;

namespace PJATKInżynierka.Models
{
    public partial class Export
    {
        public int ExportId { get; set; }
        public DateTime Date { get; set; }
        public int? NumberMale { get; set; }
        public int? NumberFemale { get; set; }
        public decimal? Weight { get; set; }
        public decimal? Price { get; set; }
        public int CycleId { get; set; }

        public virtual Cycle Cycle { get; set; } = null!;
        public virtual Delivery? Delivery { get; set; }
    }
}
