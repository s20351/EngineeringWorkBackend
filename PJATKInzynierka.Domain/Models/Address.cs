using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public partial class Address
    {
        public Address()
        {
            Farms = new HashSet<Farm>();
            Feedhouses = new HashSet<Feedhouse>();
            Hatcheries = new HashSet<Hatchery>();
        }

        public int AddressId { get; set; }
        public string City { get; set; } = null!;
        public string? Street { get; set; }
        public string? PostalCode { get; set; }
        public int? FlatNumber { get; set; }

        public virtual ICollection<Farm> Farms { get; set; }
        public virtual ICollection<Feedhouse> Feedhouses { get; set; }
        public virtual ICollection<Hatchery> Hatcheries { get; set; }
    }
}
