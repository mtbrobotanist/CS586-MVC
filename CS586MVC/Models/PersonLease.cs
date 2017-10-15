using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CS586MVC.Models
{
    public class PersonLease
    {
        public int ID { get; set; }
        public int AccountID { get; set; }
        public int LeaseID { get; set; }
        public int ComplexUnitTypeID { get; set; }
        public int Rent { get; set; }
        public DateTime LeaseStart { get; set; }
    }
}
