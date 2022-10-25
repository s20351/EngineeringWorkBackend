using System;
using System.Collections.Generic;

namespace PJATKInżynierka.Models
{
    public partial class Date
    {
        public DateTime? Date1 { get; set; }
        public bool? WorkingDate { get; set; }
        public int? DeliveryId { get; set; }
        public int? SlaughterhouseId { get; set; }
    }
}
