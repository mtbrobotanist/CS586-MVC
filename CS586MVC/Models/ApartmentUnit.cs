using System;
using System.Collections.Generic;

namespace CS586MVC.Models
{
    public partial class ApartmentUnit
    {
        public ApartmentUnit()
        {
            ApartmentComplexUnit = new HashSet<ApartmentComplexUnit>();
        }

        public int Id { get; set; }
        public int BedRooms { get; set; }
        public int Bathrooms { get; set; }
        public int? Area { get; set; }

        public ICollection<ApartmentComplexUnit> ApartmentComplexUnit { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is ApartmentUnit other)
            {
                if (Id != other.Id)
                {
                    return false;
                }
                
                if(BedRooms != other.BedRooms)
                {
                    return false;
                }

                if (Bathrooms != other.Bathrooms)
                {
                    return false;
                }

                if (!Area.HasValue && !other.Area.HasValue ||
                    this.Area.HasValue && !other.Area.HasValue ||
                    !this.Area.HasValue && other.Area.HasValue)
                {
                    return false;
                }

                return this.Area.Value == other.Area.Value;
            }
            
            return false;
        }
    }
}
