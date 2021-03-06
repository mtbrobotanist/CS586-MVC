﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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

        [NotMapped]
        public int VacancyCount => Size - ApartmentComplexUnits.Count(unit => unit.Occupied);

        [NotMapped]
        public int OccupiedCount => Size - VacancyCount;

        [NotMapped]
        public IEnumerable<ApartmentComplexUnit> VacantUnits => ApartmentComplexUnits.Where(unit => !unit.Occupied).ToList();

        [NotMapped]
        public IEnumerable<ApartmentComplexUnit> OccupiedUnits => ApartmentComplexUnits.Where(unit => unit.Occupied).ToList();

        public ICollection<ApartmentComplexUnit> ApartmentComplexUnits { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is ApartmentComplex other)
            {
                if (Id != 0 && other.Id != 0)
                {
                    return Id == other.Id;
                }
                
                return Name.Equals(other.Name)
                    && Address.Equals(other.Address) 
                    && Size == other.Size;
            }

            return false;
        }
    }
}
