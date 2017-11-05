using System;
using System.Collections.Generic;

namespace CS586MVC.Models
{
    public partial class AptUnit
    {
        public AptUnit()
        {
            AptComplexUnit = new HashSet<AptComplexUnit>();
        }

        public int Id { get; set; }
        public int BedRooms { get; set; }
        public int Bathrooms { get; set; }
        public int? Area { get; set; }

        public ICollection<AptComplexUnit> AptComplexUnit { get; set; }
    }
}
