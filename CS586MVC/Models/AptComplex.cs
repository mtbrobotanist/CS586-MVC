using System;
using System.Collections.Generic;

namespace CS586MVC.Models
{
    public partial class AptComplex
    {
        public AptComplex()
        {
            OccupiedUnits = new HashSet<AptComplexUnit>();
        }

        public int Id { get; set; }
        public string Address { get; set; }
        public int Size { get; set; }

        public int Vacancies => Size - OccupiedUnits.Count;

        public ICollection<AptComplexUnit> OccupiedUnits { get; set; }
    }
}
