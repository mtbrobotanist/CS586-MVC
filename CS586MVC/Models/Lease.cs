using System;
using Newtonsoft.Json;


namespace CS586MVC.Models
{
    public partial class Lease
    {
        public int Id { get; set; }
        public int PersonId { get; set; }
        public int AptComplexUnitId { get; set; }
        
        [JsonIgnore]
        public DateTime StartDate { get; set; }
        
        [JsonProperty("startDate")]
        public string Start => StartDate.ToShortDateString();

        public int DurationMonths { get; set; }
        public int RentMonthly { get; set; }

        public bool Active => StartDate.AddMonths(DurationMonths) > DateTime.Now;
        
        [JsonIgnore]
        public DateTime EndDate => StartDate.AddMonths(DurationMonths);

        [JsonProperty("endDate")]
        public string End => EndDate.ToShortDateString();
        
        public AptComplexUnit Unit { get; set; }
        public Person Tenant { get; set; }
    }
}
