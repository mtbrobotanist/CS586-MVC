using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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

        [NotMapped]
        public bool Current => Leases.Any(l => l.Active);

        [NotMapped]
        public Lease CurrentLease => Leases.FirstOrDefault(l => l.Active);

        [NotMapped]
        public int? CurrentLeaseId => CurrentLease?.Id;
        
        public ICollection<Lease> Leases { get; set; }

        public override bool Equals(object obj)
        {           
            if (obj is Person other)
            {
                if (Id != 0 && other.Id != 0)
                {
                    return Id == other.Id;
                }
                    
                return FirstName.Equals(other.FirstName)
                       && LastName.Equals(other.LastName)
                       && Phone.Equals(other.Phone)
                       && Email.Equals(other.Email);
            }

            return false;
        }
    }
}
