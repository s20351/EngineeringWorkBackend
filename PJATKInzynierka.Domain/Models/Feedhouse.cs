using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public partial class Feedhouse
    {
        public Feedhouse()
        {
            OrderFeeds = new HashSet<OrderFeed>();
        }

        public int FeedhouseId { get; set; }
        public int? AddressAddressId { get; set; }

        public virtual Address? AddressAddress { get; set; }
        public virtual ICollection<OrderFeed> OrderFeeds { get; set; }
    }
}
