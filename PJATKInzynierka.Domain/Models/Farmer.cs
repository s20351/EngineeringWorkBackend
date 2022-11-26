using System;
using System.Collections.Generic;

namespace Domain.Models
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
        public string FarmerColor { get; set; } = null!;
        public string? Email { get; set; }
        public string? Password { get; set; }

        public virtual ICollection<Farm> Farms { get; set; }
    }
}
