using System;
using System.Collections.Generic;

namespace Domain.Models
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
        public string Name { get; set; } = null!;
        public string FarmColor { get; set; } = null!;
        public int? AddressAddressId { get; set; }
        public int FarmerFarmerId { get; set; }

        public virtual Address? AddressAddress { get; set; }
        public virtual Farmer FarmerFarmer { get; set; } = null!;
        public virtual ICollection<Cycle> Cycles { get; set; }
        public virtual ICollection<OrderFeed> OrderFeeds { get; set; }
        public virtual ICollection<OrderHatchery> OrderHatcheries { get; set; }
    }
}
