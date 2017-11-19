using System;
using System.Collections.Generic;
using System.Linq;

namespace CS586MVC.Models
{
    public partial class ApartmentComplex
    {
        public ApartmentComplex()
        {
            AptComplexUnits = new HashSet<ApartmentComplexUnit>();
        }

        public int Id { get; set; }
        public string Address { get; set; }
        public int Size { get; set; }

        public int VacancyCount => Size - AptComplexUnits.Count(unit => unit.Occupied);
        public int OccupiedCount => Size - VacancyCount;

        public IEnumerable<ApartmentComplexUnit> VacantUnits => AptComplexUnits.Where(unit => !unit.Occupied).ToList();
        public IEnumerable<ApartmentComplexUnit> OccupiedUnits => AptComplexUnits.Where(unit => unit.Occupied).ToList();

        public ICollection<ApartmentComplexUnit> AptComplexUnits { get; set; }
    }
}
