using System;
using System.Collections.Generic;

namespace PJATKInżynierka.Models
{
    public partial class Farmer
    {
        public Farmer()
        {
            Farms = new HashSet<Farm>();
        }

        public int FarmerId { get; set; }
        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public bool KeyFarmer { get; set; }
        public string? FarmerColor { get; set; }

        public virtual ICollection<Farm> Farms { get; set; }
    }
}
