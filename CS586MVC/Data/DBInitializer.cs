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
            if (context.Leases.Any())
            {
                return;   // DB has been seeded
            }
            
            Console.WriteLine("*****Seeding Unit");
            //add the generic aprartment units
            var units = new ApartmentUnit[]
            {
                //new Student{FirstMidName="Carson",LastName="Alexander",EnrollmentDate=DateTime.Parse("2005-09-01")},
                new ApartmentUnit{BedRooms = 0, Bathrooms = 0, Area = 200},
                new ApartmentUnit{BedRooms = 0, Bathrooms = 1, Area = 230},
                new ApartmentUnit{BedRooms = 1, Bathrooms = 1, Area = 350},
                new ApartmentUnit{BedRooms = 2, Bathrooms = 0, Area = 400},
                new ApartmentUnit{BedRooms = 2, Bathrooms = 2, Area = 600},
                new ApartmentUnit{BedRooms = 3, Bathrooms = 2, Area = 900}
            };
            foreach (ApartmentUnit u in units)
            {
                context.AptUnits.Add(u);
            }
            context.SaveChanges();
            
            Console.WriteLine("*****Seeding Complex");
            //add the apartment complex locations
            var complexes = new ApartmentComplex[]
            {
                new ApartmentComplex{Address="12345 Fake Street, Somewhere, CA, 91302", Size = 30},
                new ApartmentComplex{Address="420 High Blvd, Somewhere, CA, 91302", Size = 420},
            };
            foreach(ApartmentComplex c in complexes)
            {
                context.AptComplexes.Add(c);
            }
            context.SaveChanges();
            
            Console.WriteLine("*****Seeding ComplexUnit");
            //add the apartment units that each complex has
            var complexUnits = new ApartmentComplexUnit[]
            {
                new ApartmentComplexUnit{AptUnitId = 1, AptComplexId = 1, UnitNumber = 100},
                new ApartmentComplexUnit{AptUnitId = 2, AptComplexId = 1, UnitNumber = 101},
                new ApartmentComplexUnit{AptUnitId = 3, AptComplexId = 1, UnitNumber = 102},
                new ApartmentComplexUnit{AptUnitId = 4, AptComplexId = 1, UnitNumber = 200},
                new ApartmentComplexUnit{AptUnitId = 5, AptComplexId = 1, UnitNumber = 201},
                new ApartmentComplexUnit{AptUnitId = 6, AptComplexId = 1, UnitNumber = 203},
                
                new ApartmentComplexUnit{AptUnitId = 1, AptComplexId = 2, UnitNumber = 100},
                new ApartmentComplexUnit{AptUnitId = 2, AptComplexId = 2, UnitNumber = 101},
                new ApartmentComplexUnit{AptUnitId = 3, AptComplexId = 2, UnitNumber = 102},
                new ApartmentComplexUnit{AptUnitId = 4, AptComplexId = 2, UnitNumber = 200},
                new ApartmentComplexUnit{AptUnitId = 5, AptComplexId = 2, UnitNumber = 201},
                new ApartmentComplexUnit{AptUnitId = 6, AptComplexId = 2, UnitNumber = 202}
            };
            foreach(ApartmentComplexUnit cu in complexUnits)
            {
                context.AptComplexUnits.Add(cu);
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
                context.Persons.Add(p);
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
                context.Leases.Add(l);
            }
            context.SaveChanges();
        }
    }
}

