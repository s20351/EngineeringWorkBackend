using System;
using System.Collections.Generic;

namespace PJATKInżynierka.Models
{
    public partial class Cycle
    {
        public Cycle()
        {
            Exports = new HashSet<Export>();
        }

        public int CycleId { get; set; }
        public string? Description { get; set; }
        public DateTime DateIn { get; set; }
        public DateTime? DateOut { get; set; }
        public int NumberMale { get; set; }
        public int NumberFemale { get; set; }
        public int? FarmId { get; set; }

        public virtual Farm? Farm { get; set; }
        public virtual ICollection<Export> Exports { get; set; }
    }
}
