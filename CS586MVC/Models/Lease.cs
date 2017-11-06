using System;
using System.Collections.Generic;

namespace CS586MVC.Models
{
    public partial class Lease
    {
        public int Id { get; set; }
        public int PersonId { get; set; }
        public int AptComplexUnitId { get; set; }
        public DateTime StartDate { get; set; }
        public int DurationMonths { get; set; }
        public int RentMonthly { get; set; }

        public bool Active => StartDate.AddMonths(DurationMonths) > DateTime.Now;
        
        public AptComplexUnit AptComplexUnit { get; set; }
        public Person Person { get; set; }
    }
}
