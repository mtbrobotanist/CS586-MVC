using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using CS586MVC.Data;
using CS586MVC.Models;

namespace CS586MVC.Controllers
{
    public partial class PropertyDataController : Controller
    {
       public PropertyDataController(PropertyMismanagementContext context)
        {    
            DbHelper.Context = context;
        }
        
        // GET
        public async Task<IEnumerable<Person>> Tenants(int? id)
        {
            return id.HasValue ? 
                new List<Person> { await DbHelper.Person((int)id) } : 
                await DbHelper.AllPersons();
        }
        
        // GET
        public async Task<IEnumerable<AptComplex>> Properties(int? id)
        {
            return id.HasValue ?
                new List<AptComplex> { await DbHelper.AptComplex((int)id) } :
                await DbHelper.AllAptComplexes();
        }
        
        
        // GET
        public async Task<IEnumerable<AptComplexUnit>> AllUnits(int? id)
        {
            return id.HasValue ?
                new List<AptComplexUnit> { await DbHelper.AptComplexUnit((int)id) } :
                await DbHelper.AllAptComplexUnits();
        }
        
        // GET
        public async Task<IEnumerable<Lease>> Leases(int? id)
        {
            return id.HasValue ? 
                new List<Lease> { await DbHelper.Lease((int) id) } : 
                await DbHelper.AllLeases();
        }
        
        //PUT
        [HttpPut] [HttpPost]
        public async Task CreateTenant(Person p)
        {
            Console.WriteLine($"Received new Person: {p.FirstName}, {p.LastName}");
            await DbHelper.InsertPerson(p);
        }

        [HttpPut] [HttpPost]
        public async Task CreateLease(Lease l)
        {
            Console.WriteLine($"Received new Lease: {l}");
        }
        
        [HttpPut] [HttpPost]
        public async Task CreateApartmentUnit(AptComplexUnit acu)
        {
            Console.WriteLine($"Received new AptComplexUnit:{acu}");
        }
        
        [HttpPut] [HttpPost]
        public async Task CreateProperty(AptComplex ac)
        {
            Console.WriteLine($"Received new AptComplex:{ac.Address}, {ac.Size}");
            await DbHelper.InsertAptComplex(ac);
        }
        
    }
}
