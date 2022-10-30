using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public partial class Cycle
    {
        public Cycle()
        {
            Exports = new HashSet<Export>();
        }

        public int CycleId { get; set; }
        public string Description { get; set; } = null!;
        public DateTime DateIn { get; set; }
        public DateTime DateOut { get; set; }
        public int? NumberMale { get; set; }
        public int? NumberFemale { get; set; }
        public int FarmFarmId { get; set; }

        public virtual Farm FarmFarm { get; set; } = null!;
        public virtual ICollection<Export> Exports { get; set; }
    }
}
