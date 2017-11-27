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
                        
            Console.WriteLine("*****Seeding Complex");
            //add the apartment complex locations
            var complexes = new ApartmentComplex[]
            {
                new ApartmentComplex{Address="12345 Fake Street, Somewhere, CA, 90210", Size = 30, Name="The Plastic Terrace"},
                new ApartmentComplex{Address="420 Highland Blvd, Somewhere, CA, 91302", Size = 420, Name="The Smokey Chimney"},
            };
            foreach(ApartmentComplex c in complexes)
            {
                context.ApartmentComplexes.Add(c);
            }
            context.SaveChanges();
            
            Console.WriteLine("*****Seeding ComplexUnit");
            //add the apartment units that each complex has
            var complexUnits = new ApartmentComplexUnit[]
            {           
                new ApartmentComplexUnit{ApartmentComplexId = 1, UnitNumber = 100, BedRooms = 1, BathRooms = 1, Area = 600,},
                new ApartmentComplexUnit{ApartmentComplexId = 1, UnitNumber = 101, BedRooms = 2, BathRooms = 1, Area = 700,},
                new ApartmentComplexUnit{ApartmentComplexId = 1, UnitNumber = 102, BedRooms = 3, BathRooms = 2, Area = 800,},
                new ApartmentComplexUnit{ApartmentComplexId = 1, UnitNumber = 200, BedRooms = 1, BathRooms = 1, Area = 900,},
                new ApartmentComplexUnit{ApartmentComplexId = 1, UnitNumber = 201, BedRooms = 2, BathRooms = 2, Area = 1000,},
                new ApartmentComplexUnit{ApartmentComplexId = 1, UnitNumber = 203, BedRooms = 3, BathRooms = 2, Area = 1100,},
                
                new ApartmentComplexUnit{ApartmentComplexId = 2, UnitNumber = 100, BedRooms = 3, BathRooms = 2, Area = 1200,},
                new ApartmentComplexUnit{ApartmentComplexId = 2, UnitNumber = 101, BedRooms = 2, BathRooms = 2, Area = 1500,},
                new ApartmentComplexUnit{ApartmentComplexId = 2, UnitNumber = 102, BedRooms = 1, BathRooms = 1, Area = 1100,},
                new ApartmentComplexUnit{ApartmentComplexId = 2, UnitNumber = 200, BedRooms = 3, BathRooms = 2, Area = 1000,},
                new ApartmentComplexUnit{ApartmentComplexId = 2, UnitNumber = 201, BedRooms = 2, BathRooms = 2, Area = 900,},
                new ApartmentComplexUnit{ApartmentComplexId = 2, UnitNumber = 202, BedRooms = 2, BathRooms = 1, Area = 800,}
            };
            foreach(ApartmentComplexUnit cu in complexUnits)
            {
                context.ApartmentComplexUnits.Add(cu);
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
                context.People.Add(p);
            }
            context.SaveChanges();
            
            
            Console.WriteLine("*****Seeding Lease");
            //finally, add the leases
            var leases = new Lease[]
            {
                new Lease {PersonId = 1, ApartmentComplexUnitId = 1, StartDate=DateTimeHelper(), DurationMonths = 12, RentMonthly = 1000},
                new Lease {PersonId = 2, ApartmentComplexUnitId = 2, StartDate=DateTimeHelper(), DurationMonths = 6, RentMonthly = 1300},
                new Lease {PersonId = 3, ApartmentComplexUnitId = 3, StartDate=DateTimeHelper(), DurationMonths = 12, RentMonthly = 900},
                new Lease {PersonId = 4, ApartmentComplexUnitId = 7, StartDate=DateTimeHelper(), DurationMonths = 6, RentMonthly = 1700},
                new Lease {PersonId = 5, ApartmentComplexUnitId = 8, StartDate=DateTimeHelper(), DurationMonths = 12, RentMonthly = 1100},
                new Lease {PersonId = 6, ApartmentComplexUnitId = 9, StartDate=DateTimeHelper(), DurationMonths = 6, RentMonthly = 1000}

            };
            foreach (Lease l in leases)
            {
                context.Leases.Add(l);
            }
            context.SaveChanges();
        }

        private static long DateTimeHelper()
        {
            return (long) (DateTime.Now - Lease.UnixEpoch).TotalMilliseconds;
        }
    }
}

