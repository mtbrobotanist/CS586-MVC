﻿using System;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;


namespace CS586MVC.Models
{
    public partial class Lease
    {

        [JsonIgnore]
        [NotMapped]
        public static readonly DateTime UnixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public int Id { get; set; }

        [JsonProperty("tenantId")]
        public int PersonId { get; set; }

        public int ApartmentComplexUnitId { get; set; }

        [JsonProperty("startDate")]
        public long StartDate { get; set; }

        public int DurationMonths { get; set; }
        public int RentMonthly { get; set; }

        [NotMapped]
        public bool Active => __startDateTime.AddMonths(DurationMonths) > DateTime.Now;

        [JsonIgnore]
        [NotMapped]
        private DateTime __startDateTime => UnixEpoch.AddMilliseconds(StartDate).ToLocalTime();
        
        public ApartmentComplexUnit ApartmentComplexUnit { get; set; }

        [JsonProperty("tenant")]
        public Person Person { get; set; }
    }
}
