using System;
using Newtonsoft.Json;


namespace CS586MVC.Models
{
    public partial class Lease
    {
        private static readonly DateTime unixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        
        public int Id { get; set; }
        public int PersonId { get; set; }
        public int ApartmentComplexUnitId { get; set; }
        
        [JsonIgnore]
        public DateTime StartDate { get; set; }

        [JsonProperty("startDate")]
        public long __Start => (long) (StartDate - unixEpoch).TotalMilliseconds;

        public int DurationMonths { get; set; }
        public int RentMonthly { get; set; }

        public bool Active => StartDate.AddMonths(DurationMonths) > DateTime.Now;
        
        [JsonIgnore]
        public DateTime EndDate => StartDate.AddMonths(DurationMonths);

        [JsonProperty("endDate")]
        public string __End => EndDate.ToShortDateString();
        
        public ApartmentComplexUnit ApartmentComplexUnit { get; set; }
        public Person Tenant { get; set; }
    }
}
