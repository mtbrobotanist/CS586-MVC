using System;
using System.Collections.Generic;
using System.Linq;

namespace CS586MVC.Models
{
    public partial class ApartmentComplexUnit
    {
        public ApartmentComplexUnit()
        {
            Leases = new HashSet<Lease>();
        }

        public int Id { get; set; }
        public int? AptUnitId { get; set; }
        public int? AptComplexId { get; set; }
        public int UnitNumber { get; set; }

        public bool Occupied => Leases.Any(l => l.Active);

        public string Address => $"#{UnitNumber}, {ApartmentComplex?.Address}";
        
        public ApartmentComplex ApartmentComplex { get; set; }
        public ApartmentUnit ApartmentUnit { get; set; }
        public ICollection<Lease> Leases { get; set; }
    }
    
}
