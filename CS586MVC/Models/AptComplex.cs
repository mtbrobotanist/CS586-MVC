using System;
using System.Collections.Generic;
using System.Linq;

namespace CS586MVC.Models
{
    public partial class AptComplex
    {
        public AptComplex()
        {
            Units = new HashSet<AptComplexUnit>();
        }

        public int Id { get; set; }
        public string Address { get; set; }
        public int Size { get; set; }

        public int VacancyCount => Size - Units.Count(unit => unit.Occupied);
        public int OccupiedCount => Size - VacancyCount;

        public IEnumerable<AptComplexUnit> VacantUnits => Units.Where(unit => !unit.Occupied).ToList();
        public IEnumerable<AptComplexUnit> OccupiedUnits => Units.Where(unit => unit.Occupied).ToList();

        public ICollection<AptComplexUnit> Units { get; set; }
    }
}
