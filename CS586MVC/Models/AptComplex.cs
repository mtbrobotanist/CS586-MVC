using System;
using System.Collections.Generic;
using System.Linq;

namespace CS586MVC.Models
{
    public partial class AptComplex
    {
        public AptComplex()
        {
            AptComplexUnits = new HashSet<AptComplexUnit>();
        }

        public int Id { get; set; }
        public string Address { get; set; }
        public int Size { get; set; }

        public int VacancyCount => Size - AptComplexUnits.Count(unit => unit.Occupied);
        public int OccupiedCount => Size - VacancyCount;

        public IEnumerable<AptComplexUnit> VacantUnits => AptComplexUnits.Where(unit => !unit.Occupied).ToList();
        public IEnumerable<AptComplexUnit> OccupiedUnits => AptComplexUnits.Where(unit => unit.Occupied).ToList();

        public ICollection<AptComplexUnit> AptComplexUnits { get; set; }
    }
}
