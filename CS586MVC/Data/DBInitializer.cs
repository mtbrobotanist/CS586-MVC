using System;
using System.Linq;
using CS586MVC.Models;

namespace CS586MVC.Data
{
    public static class DbInitializer
    {
        public static void Initialize(PropertyMismanagementContext context)
        {
            context.Database.EnsureCreated();
                
            // Look for any leases.
            if (context.Lease.Any())
            {
                return;   // DB has been seeded
            }
            
            Console.WriteLine("*****Seeding Unit");
            //add the generic aprartment units
            var units = new Unit[]
            {
                //new Student{FirstMidName="Carson",LastName="Alexander",EnrollmentDate=DateTime.Parse("2005-09-01")},
                new Unit{BedRooms = 0, Bathrooms = 0, Area = 200},
                new Unit{BedRooms = 0, Bathrooms = 1, Area = 230},
                new Unit{BedRooms = 1, Bathrooms = 1, Area = 350},
                new Unit{BedRooms = 2, Bathrooms = 0, Area = 400},
                new Unit{BedRooms = 2, Bathrooms = 2, Area = 600},
                new Unit{BedRooms = 3, Bathrooms = 2, Area = 900}
            };
            foreach (Unit u in units)
            {
                context.Unit.Add(u);
            }
            context.SaveChanges();
            
            Console.WriteLine("*****Seeding Complex");
            //add the apartment complex locations
            var complexes = new Complex[]
            {
                new Complex{Address="12345 Fake Street, Somewhere, CA, 91302"},
                new Complex{Address="420 High Blvd, Somewhere, CA, 91302"},
            };
            foreach(Complex c in complexes)
            {
                context.Complex.Add(c);
            }
            context.SaveChanges();
            
            Console.WriteLine("*****Seeding ComplexUnit");
            //add the apartment units that each complex has
            var complexUnits = new ComplexUnit[]
            {
                new ComplexUnit{UnitId = 1, ComplexId = 1},
                new ComplexUnit{UnitId = 2, ComplexId = 1},
                new ComplexUnit{UnitId = 3, ComplexId = 1},
                new ComplexUnit{UnitId = 4, ComplexId = 1},
                new ComplexUnit{UnitId = 5, ComplexId = 1},
                new ComplexUnit{UnitId = 6, ComplexId = 1},
                
                new ComplexUnit{UnitId = 1, ComplexId = 2},
                new ComplexUnit{UnitId = 2, ComplexId = 2},
                new ComplexUnit{UnitId = 3, ComplexId = 2},
                new ComplexUnit{UnitId = 4, ComplexId = 2},
                new ComplexUnit{UnitId = 5, ComplexId = 2},
                new ComplexUnit{UnitId = 6, ComplexId = 2}
            };
            foreach(ComplexUnit cu in complexUnits)
            {
                context.ComplexUnit.Add(cu);
            }
            context.SaveChanges();
            
            Console.WriteLine("*****Seeding Person");
            //add the tenants 
            var persons = new Person[]
            {
                new Person{FirstName = "Nate", LastName = "Wilson", Phone="8184208888", Email = "no@nope.com"},
                new Person{FirstName = "Edwin", LastName = "Portillo", Phone="8181118888", Email = "yes@yep.com"},
                new Person{FirstName = "Dragos", LastName = "Guta", Phone="8184208888", Email = "guta@guta.com"},
                new Person{FirstName = "OG-Sub", LastName = "Chung", Phone="2134209699", Email = "chungWhoa@my.domain"},
                new Person{FirstName = "Felix", LastName = "Rabinovich", Phone="8184204444", Email = "felix.rabinovich@somewhere.com"},
                new Person{FirstName = "Hazeem", LastName = "Rekab", Phone="8008675309", Email = "no@nope.com"},
            };
            foreach (Person p in persons)
            {
                context.Person.Add(p);
            }
            context.SaveChanges();
            
            
            Console.WriteLine("*****Seeding Lease");
            //finally, add the leases
            var leases = new Lease[]
            {
                new Lease {PersonId = 1, ComplexUnitId = 1, StartDate=DateTime.Now, DurationMonths = 12, RentMonthly = 1000},
                new Lease {PersonId = 2, ComplexUnitId = 2, StartDate=DateTime.Now, DurationMonths = 6, RentMonthly = 1300},
                new Lease {PersonId = 3, ComplexUnitId = 3, StartDate=DateTime.Now, DurationMonths = 12, RentMonthly = 900},
                new Lease {PersonId = 4, ComplexUnitId = 7, StartDate=DateTime.Now, DurationMonths = 6, RentMonthly = 1700},
                new Lease {PersonId = 5, ComplexUnitId = 8, StartDate=DateTime.Now, DurationMonths = 12, RentMonthly = 1100},
                new Lease {PersonId = 6, ComplexUnitId = 9, StartDate=DateTime.Now, DurationMonths = 6, RentMonthly = 1000}

            };
            foreach (Lease l in leases)
            {
                context.Lease.Add(l);
            }
            context.SaveChanges();
        }
    }
}

