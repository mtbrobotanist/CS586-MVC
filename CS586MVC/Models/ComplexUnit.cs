using System;
using System.Collections.Generic;

namespace CS586MVC.Models
{
    public partial class ComplexUnit
    {
        public ComplexUnit()
        {
            Lease = new HashSet<Lease>();
        }

        public int Id { get; set; }
        public int? UnitId { get; set; }
        public int? ComplexId { get; set; }
        public int UnitNumber { get; set; }

        public Complex Complex { get; set; }
        public Unit Unit { get; set; }
        public ICollection<Lease> Lease { get; set; }
    }
}
