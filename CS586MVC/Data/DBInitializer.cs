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
            var units = new AptUnit[]
            {
                //new Student{FirstMidName="Carson",LastName="Alexander",EnrollmentDate=DateTime.Parse("2005-09-01")},
                new AptUnit{BedRooms = 0, Bathrooms = 0, Area = 200},
                new AptUnit{BedRooms = 0, Bathrooms = 1, Area = 230},
                new AptUnit{BedRooms = 1, Bathrooms = 1, Area = 350},
                new AptUnit{BedRooms = 2, Bathrooms = 0, Area = 400},
                new AptUnit{BedRooms = 2, Bathrooms = 2, Area = 600},
                new AptUnit{BedRooms = 3, Bathrooms = 2, Area = 900}
            };
            foreach (AptUnit u in units)
            {
                context.AptUnit.Add(u);
            }
            context.SaveChanges();
            
            Console.WriteLine("*****Seeding Complex");
            //add the apartment complex locations
            var complexes = new AptComplex[]
            {
                new AptComplex{Address="12345 Fake Street, Somewhere, CA, 91302", Size = 30},
                new AptComplex{Address="420 High Blvd, Somewhere, CA, 91302", Size = 420},
            };
            foreach(AptComplex c in complexes)
            {
                context.AptComplex.Add(c);
            }
            context.SaveChanges();
            
            Console.WriteLine("*****Seeding ComplexUnit");
            //add the apartment units that each complex has
            var complexUnits = new AptComplexUnit[]
            {
                new AptComplexUnit{AptUnitId = 1, AptComplexId = 1, UnitNumber = 100},
                new AptComplexUnit{AptUnitId = 2, AptComplexId = 1, UnitNumber = 101},
                new AptComplexUnit{AptUnitId = 3, AptComplexId = 1, UnitNumber = 102},
                new AptComplexUnit{AptUnitId = 4, AptComplexId = 1, UnitNumber = 200},
                new AptComplexUnit{AptUnitId = 5, AptComplexId = 1, UnitNumber = 201},
                new AptComplexUnit{AptUnitId = 6, AptComplexId = 1, UnitNumber = 203},
                
                new AptComplexUnit{AptUnitId = 1, AptComplexId = 2, UnitNumber = 100},
                new AptComplexUnit{AptUnitId = 2, AptComplexId = 2, UnitNumber = 101},
                new AptComplexUnit{AptUnitId = 3, AptComplexId = 2, UnitNumber = 102},
                new AptComplexUnit{AptUnitId = 4, AptComplexId = 2, UnitNumber = 200},
                new AptComplexUnit{AptUnitId = 5, AptComplexId = 2, UnitNumber = 201},
                new AptComplexUnit{AptUnitId = 6, AptComplexId = 2, UnitNumber = 202}
            };
            foreach(AptComplexUnit cu in complexUnits)
            {
                context.AptComplexUnit.Add(cu);
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
                new Lease {PersonId = 1, AptComplexUnitId = 1, StartDate=DateTime.Now, DurationMonths = 12, RentMonthly = 1000},
                new Lease {PersonId = 2, AptComplexUnitId = 2, StartDate=DateTime.Now, DurationMonths = 6, RentMonthly = 1300},
                new Lease {PersonId = 3, AptComplexUnitId = 3, StartDate=DateTime.Now, DurationMonths = 12, RentMonthly = 900},
                new Lease {PersonId = 4, AptComplexUnitId = 7, StartDate=DateTime.Now, DurationMonths = 6, RentMonthly = 1700},
                new Lease {PersonId = 5, AptComplexUnitId = 8, StartDate=DateTime.Now, DurationMonths = 12, RentMonthly = 1100},
                new Lease {PersonId = 6, AptComplexUnitId = 9, StartDate=DateTime.Now, DurationMonths = 6, RentMonthly = 1000}

            };
            foreach (Lease l in leases)
            {
                context.Lease.Add(l);
            }
            context.SaveChanges();
        }
    }
}

