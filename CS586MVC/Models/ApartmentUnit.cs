using System;
using System.Collections.Generic;

namespace CS586MVC.Models
{
    public partial class ApartmentUnit
    {
        public ApartmentUnit()
        {
            AptComplexUnit = new HashSet<ApartmentComplexUnit>();
        }

        public int Id { get; set; }
        public int BedRooms { get; set; }
        public int Bathrooms { get; set; }
        public int? Area { get; set; }

        public ICollection<ApartmentComplexUnit> AptComplexUnit { get; set; }
    }
}
