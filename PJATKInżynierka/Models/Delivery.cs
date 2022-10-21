using System;
using System.Collections.Generic;

namespace PJATKInżynierka.Models
{
    public partial class Delivery
    {
        public int DeliveryId { get; set; }
        public int ExportId { get; set; }
        public DateTime DateDelivery { get; set; }

        public virtual DateDelivery DateDeliveryNavigation { get; set; } = null!;
        public virtual Export Export { get; set; } = null!;
    }
}
