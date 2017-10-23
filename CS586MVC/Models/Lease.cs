using System;
using System.Collections.Generic;

namespace CS586MVC.Models
{
    public partial class Lease
    {
        public int Id { get; set; }
        public int? PersonId { get; set; }
        public int? ComplexUnitId { get; set; }
        public DateTime StartDate { get; set; }
        public int DurationMonths { get; set; }
        public int RentMonthly { get; set; }

        public ComplexUnit ComplexUnit { get; set; }
        public Person Person { get; set; }
    }
}
