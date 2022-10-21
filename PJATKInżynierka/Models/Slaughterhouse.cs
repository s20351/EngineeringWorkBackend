using System;
using System.Collections.Generic;

namespace PJATKInżynierka.Models
{
    public partial class Slaughterhouse
    {
        public Slaughterhouse()
        {
            DateDeliveries = new HashSet<DateDelivery>();
        }

        public int SlaughterhouseId { get; set; }
        public string? Address { get; set; }

        public virtual ICollection<DateDelivery> DateDeliveries { get; set; }
    }
}
