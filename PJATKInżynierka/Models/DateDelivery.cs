using System;
using System.Collections.Generic;

namespace PJATKInżynierka.Models
{
    public partial class DateDelivery
    {
        public DateDelivery()
        {
            Deliveries = new HashSet<Delivery>();
        }

        public DateTime DateDelivery1 { get; set; }
        public bool? WorkingDate { get; set; }
        public int? SlaughterhouseId { get; set; }

        public virtual Slaughterhouse? Slaughterhouse { get; set; }
        public virtual ICollection<Delivery> Deliveries { get; set; }
    }
}
