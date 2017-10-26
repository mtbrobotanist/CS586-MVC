using System;
using System.Collections.Generic;

namespace CS586MVC.Models
{
    public partial class Unit
    {
        public Unit()
        {
            ComplexUnit = new HashSet<ComplexUnit>();
        }

        public int Id { get; set; }
        public int BedRooms { get; set; }
        public int Bathrooms { get; set; }
        public int? Area { get; set; }

        public ICollection<ComplexUnit> ComplexUnit { get; set; }
    }
}
