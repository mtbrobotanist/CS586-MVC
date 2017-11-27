using System;
using System.Collections.Generic;
using System.Linq;

namespace CS586MVC.Models
{
    public partial class ApartmentComplex
    {
        public ApartmentComplex()
        {
            ApartmentComplexUnits = new HashSet<ApartmentComplexUnit>();
        }

        public int Id { get; set; }
        public string Address { get; set; }
        public int Size { get; set; }
        public string Name { get; set; }

        public int VacancyCount => Size - ApartmentComplexUnits.Count(unit => unit.Occupied);
        public int OccupiedCount => Size - VacancyCount;

        public IEnumerable<ApartmentComplexUnit> VacantUnits => ApartmentComplexUnits.Where(unit => !unit.Occupied).ToList();
        public IEnumerable<ApartmentComplexUnit> OccupiedUnits => ApartmentComplexUnits.Where(unit => unit.Occupied).ToList();

        public ICollection<ApartmentComplexUnit> ApartmentComplexUnits { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is ApartmentComplex other)
            {
                return Id == other.Id
                    && Address == other.Address 
                    && Size == other.Size;
            }

            return false;
        }
    }
}
