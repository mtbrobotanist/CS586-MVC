using System;
using System.Collections.Generic;

namespace CS586MVC.Models
{
    public partial class Person
    {
        public Person()
        {
            Lease = new HashSet<Lease>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        public ICollection<Lease> Lease { get; set; }
    }
}
