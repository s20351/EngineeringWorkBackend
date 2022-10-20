using System;
using System.Collections.Generic;

namespace PJATKInżynierka.Models
{
    public partial class Delivery
    {
        public int DeliveryId { get; set; }
        public int? ExportId { get; set; }

        public virtual Export? Export { get; set; }
    }
}
