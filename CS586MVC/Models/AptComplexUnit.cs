using System;
using System.Collections.Generic;

namespace CS586MVC.Models
{
    public partial class AptComplexUnit
    {
        public AptComplexUnit()
        {
            Lease = new HashSet<Lease>();
        }

        public int Id { get; set; }
        public int? AptUnitId { get; set; }
        public int? AptComplexId { get; set; }
        public int UnitNumber { get; set; }

        public AptComplex AptComplex { get; set; }
        public AptUnit AptUnit { get; set; }
        public ICollection<Lease> Lease { get; set; }
    }
}
