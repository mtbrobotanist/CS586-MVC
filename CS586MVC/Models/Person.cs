using System;
using System.Collections.Generic;
using System.Linq;

namespace CS586MVC.Models
{
    public partial class Person
    {
        public Person()
        {
            Leases = new HashSet<Lease>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        public bool Current => Leases.Any(l => l.Active);
        public Lease CurrentLease => Leases.FirstOrDefault(l => l.Active);
        public int CurrentLeaseId => CurrentLease.Id;
        
        public ICollection<Lease> Leases { get; set; }
    }
}
