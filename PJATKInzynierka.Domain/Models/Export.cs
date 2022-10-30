using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public partial class Export
    {
        public int ExportId { get; set; }
        public int? NumberMale { get; set; }
        public int? NumberFemale { get; set; }
        public decimal Weight { get; set; }
        public int? TermTermId { get; set; }
        public int CycleCycleId { get; set; }

        public virtual Cycle CycleCycle { get; set; } = null!;
        public virtual Term? TermTerm { get; set; }
    }
}
