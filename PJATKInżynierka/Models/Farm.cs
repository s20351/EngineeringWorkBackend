using System;
using System.Collections.Generic;

namespace PJATKInżynierka.Models
{
    public partial class Farm
    {
        public Farm()
        {
            Cycles = new HashSet<Cycle>();
            OrderFeeds = new HashSet<OrderFeed>();
            OrderHatcheries = new HashSet<OrderHatchery>();
        }

        public int FarmId { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? FarmColor { get; set; }
        public int? FarmerId { get; set; }

        public virtual Farmer? Farmer { get; set; }
        public virtual ICollection<Cycle> Cycles { get; set; }
        public virtual ICollection<OrderFeed> OrderFeeds { get; set; }
        public virtual ICollection<OrderHatchery> OrderHatcheries { get; set; }
    }
}
