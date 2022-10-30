using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public partial class Hatchery
    {
        public Hatchery()
        {
            OrderHatcheries = new HashSet<OrderHatchery>();
        }

        public int HatcheryId { get; set; }
        public int? AddressAddressId { get; set; }

        public virtual Address? AddressAddress { get; set; }
        public virtual ICollection<OrderHatchery> OrderHatcheries { get; set; }
    }
}
