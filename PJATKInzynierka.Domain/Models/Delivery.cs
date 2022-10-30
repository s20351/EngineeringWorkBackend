using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public partial class Delivery
    {
        public int DeliveryId { get; set; }
        public decimal Weight { get; set; }
        public int TermTermId { get; set; }

        public virtual Term TermTerm { get; set; } = null!;
    }
}
