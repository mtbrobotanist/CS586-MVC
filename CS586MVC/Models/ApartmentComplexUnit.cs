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
        public int BedRooms { get; set; }
        public int BathRooms { get; set; }
        public int Area { get; set; }
        public int? ApartmentComplexId { get; set; }
        public int UnitNumber { get; set; }

        public bool Occupied => Leases.Any(l => l.Active);

        public string Address => $"#{UnitNumber}, {ApartmentComplex?.Address}";
        
        public ApartmentComplex ApartmentComplex { get; set; }
        public ICollection<Lease> Leases { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is ApartmentComplexUnit other)
            {
                if (other.Id != 0)
                {
                    return Id == other.Id;
                }

                bool aptComplexIdSame = ApartmentComplexId.HasValue
                                        && other.ApartmentComplexId.HasValue
                                        && ApartmentComplexId.Value == other.ApartmentComplexId.Value;
                
                return aptComplexIdSame 
                       && BedRooms == other.BedRooms
                       && BathRooms == other.BathRooms
                       && Area == other.Area
                       && UnitNumber == other.UnitNumber;
            }
            
            return false;
        }
    }
    
}
