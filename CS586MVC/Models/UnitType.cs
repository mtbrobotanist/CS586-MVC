using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CS586MVC.Models
{
    public class UnitType
    {
        public int ID { get; set; }
        public int BedRooms { get; set; }
        public int BathRooms { get; set; }
        public int SquareFootage { get; set; }
    }
}
