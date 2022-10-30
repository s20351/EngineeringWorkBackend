using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public partial class Term
    {
        public Term()
        {
            Deliveries = new HashSet<Delivery>();
            Exports = new HashSet<Export>();
        }

        public int TermId { get; set; }
        public DateTime Date { get; set; }
        public bool IsWorkingDay { get; set; }

        public virtual ICollection<Delivery> Deliveries { get; set; }
        public virtual ICollection<Export> Exports { get; set; }
    }
}
