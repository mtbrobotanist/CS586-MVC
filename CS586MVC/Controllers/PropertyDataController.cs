using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using CS586MVC.Models;
using CS586MVC.Services;

namespace CS586MVC.Controllers
{
    public partial class PropertyDataController : Controller
    {
        private IDatabaseService _dbService;
        
        public PropertyDataController(IDatabaseService databaseService)
        {
            this._dbService = databaseService;
        }
        
        [HttpGet]
        public async Task<IEnumerable<ApartmentComplex>> Properties(int? id)
        {
            return id.HasValue ?
                new List<ApartmentComplex> { await _dbService.ApartmentComplex((int)id) } :
                await _dbService.AllApartmentComplexes();
        }
        
        [HttpPost]
        public async Task Properties(ApartmentComplex ac)
        {
            Console.WriteLine($"Received new AptComplex:{ac.Address}, {ac.Size}");
            await _dbService.InsertApartmentComplex(ac);
        }
        
        [HttpPut]
        public async Task Properties(int id, [FromBody] ApartmentComplex ac)
        {
            Console.WriteLine($"Updating Existing AptComplex: {ac.Address}, {ac.Size}");
            await _dbService.UpdateApartmentComplex(id, ac);
        }

        [HttpDelete]
        public async Task Properties(int id)
        {
            Console.WriteLine($"Deleting AptComplex with ID:{id}");
            await _dbService.RemoveApartmentComplex(id);
        }

        [HttpGet]
        public async Task<IEnumerable<ApartmentComplexUnit>> PropertyUnits(int? id)
        {
            return id.HasValue ?
                new List<ApartmentComplexUnit> { await _dbService.ApartmentComplexUnit((int)id) } :
                await _dbService.AllApartmentComplexUnits();
        }
        
        [HttpPost]
        public async Task PropertyUnits([FromBody] ApartmentComplexUnit acu)
        {
            Console.WriteLine($"Received new AptComplexUnit:{acu}");
            await _dbService.InsertApartmentComplexUnit(acu);
        }

        [HttpPut]
        public async Task PropertyUnits(int id, [FromBody] ApartmentComplexUnit acu)
        {
            
        }
        
        [HttpDelete]
        public async Task PropertyUnits(int id)
        {
            //todo
        }

        [HttpGet]
        public async Task<IEnumerable<Person>> Tenants(int? id)
        {
            return id.HasValue ? 
                new List<Person> { await _dbService.Person((int)id) } : 
                await _dbService.AllPersons();
        }
        
        [HttpPost]
        public async Task Tenants([FromBody]Person p)
        {
            Console.WriteLine($"Received new Person: {p.FirstName} {p.LastName}");
            await _dbService.InsertPerson(p);
        }
        
        [HttpPut]
        public async Task Tenants(int id, [FromBody]Person p)
        {
            Console.WriteLine($"Updating existing person with id:{id}");
            await _dbService.UpdatePerson(id, p);
        }

        [HttpDelete]
        public async Task Tenants(int id)
        {
            await _dbService.RemovePerson(id);
        }

        [HttpGet]
        public async Task<IEnumerable<Lease>> Leases(int? id)
        {
            return id.HasValue ? 
                new List<Lease> { await _dbService.Lease((int) id) } : 
                await _dbService.AllLeases();
        }
        
        [HttpPost]
        public async Task Leases(Lease l)
        {
            Console.WriteLine($"Received new Lease: {l}");
            await _dbService.InsertLease(l);
        }

        [HttpPut]
        public async Task Leases(int id, Lease l)
        {
            
        }

        [HttpDelete]
        public async Task Leases(int id)
        {
            await _dbService.RemoveLease(id);
        }
        
    }
}
