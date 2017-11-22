using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

using CS586MVC.Data;
using CS586MVC.Models;

namespace CS586MVC.Services
{
    public class PropertyMismanagementDatabaseService : IDatabaseService
    {
        private PropertyMismanagementContext _context;
        
        public PropertyMismanagementDatabaseService(PropertyMismanagementContext context)
        {
            this._context = context;
        }

        public  async Task<int> InsertApartmentComplex(ApartmentComplex ac)
        {
            var previousEntry = _context.ApartmentComplexes.FirstOrDefaultAsync(
                    newAc => newAc.Address == ac.Address && newAc.Size == ac.Size);

            if (previousEntry != null)
            {
                return previousEntry.Id;
            }
            
            EntityEntry<ApartmentComplex> entry = _context.ApartmentComplexes.Add(ac);
            await _context.SaveChangesAsync();
            return entry.Entity.Id;
        }

        public  async Task<ApartmentComplex> ApartmentComplex(int id, bool include = true)
        {
            if (include)
            {
                return await _context.ApartmentComplexes
                    .Include(e => e.ApartmentComplexUnits)
                        .ThenInclude(e => e.Leases)
                    .FirstOrDefaultAsync(e => e.Id == id);
            }
            
            return await _context.ApartmentComplexes.FirstOrDefaultAsync();
        }
        
        public  async Task<IEnumerable<ApartmentComplex>> AllApartmentComplexes(bool include = true)
        {
            if (include)
            {
                return await _context.ApartmentComplexes
                    .Include(e => e.ApartmentComplexUnits)
                        .ThenInclude(e => e.Leases)
                    .ToListAsync();
            }
                        
            return await _context.ApartmentComplexes.ToListAsync();
        }

        public  async Task UpdateApartmentComplex(int id, ApartmentComplex ac)
        {
            ApartmentComplex target = await _context.ApartmentComplexes.FirstAsync(a => a.Id == id);
            target.Address = ac.Address;
            target.Size = ac.Size;
            await _context.SaveChangesAsync();
        }

        public  async Task RemoveApartmentComplex(int id)
        {
            //shameful
            ApartmentComplex target = await _context.ApartmentComplexes
                .Include(e => e.ApartmentComplexUnits)
                    .ThenInclude(e => e.Leases)
                        .ThenInclude(e => e.Tenant)
                .FirstOrDefaultAsync(a => a.Id == id);

            if(target == null)
            {
                return;
            }

            /*
                When an AptComplex is deleted, ALL associated data needs to go:
                    AptComplexUnits,
                    Leases,
                    People
             */

            // For some reason this needs to be done manually
            foreach(ApartmentComplexUnit unit in target.ApartmentComplexUnits)
            {
                foreach(Lease lease in unit.Leases)
                {
                    _context.Entry(lease.Tenant).State = EntityState.Deleted;
                    _context.Entry(lease).State = EntityState.Deleted;
                }
                _context.Entry(unit).State = EntityState.Deleted;
            }
            
            _context.Entry(target).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }

        public  async Task InsertApartmentComplexUnit(ApartmentComplexUnit acu)
        {
            bool complex = acu.ApartmentComplex != null;
            bool complexId = acu.ApartmentComplexId.HasValue;
            
            if (!complex && !complexId)
            {
                throw new Exception("an AptComplexUnit object needs an associated AptComplex object!");
            }

            if(complex)
            {
                int id = await InsertApartmentComplex(acu.ApartmentComplex);
                acu.ApartmentComplexId = id;
            }
            
            _context.ApartmentComplexUnits.Add(acu);
            await _context.SaveChangesAsync();
        }
        
        public  async Task <ApartmentComplexUnit> ApartmentComplexUnit(int id, bool include = true)
        {
            if (include)
            {
                return await _context.ApartmentComplexUnits
                    .Include(e => e.ApartmentComplex)
                    .FirstOrDefaultAsync(e => e.Id == id);
            }
            
            return await _context.ApartmentComplexUnits.FirstOrDefaultAsync(e => e.Id == id);
        }
        
        public  async Task<IEnumerable<ApartmentComplexUnit>> AllApartmentComplexUnits(bool include = true)
        {
            return include ? 
                await _context.ApartmentComplexUnits.Include(e => e.ApartmentComplex).ToListAsync() :    
                await _context.ApartmentComplexUnits.ToListAsync();
        }

        public async Task UpdateApartmentComplexUnit(int id, ApartmentComplexUnit acu)
        {
            
        }

        public Task RemoveApartmentComplexUnit(int id)
        {
            throw new NotImplementedException();
        }
    
        public  async Task InsertPerson(Person p)
        {
            _context.People.Add(p);
            await _context.SaveChangesAsync();
        }

        public  async Task<Person> Person(int id, bool include = true)
        {
            if (include)
            {
                return await _context.People
                    .Include(p => p.Leases)
                        .ThenInclude(apt => apt.Unit)
                            .ThenInclude(unit => unit.ApartmentComplex)
                    .FirstOrDefaultAsync(p => p.Id == id);
            }

            return await _context.People.FirstOrDefaultAsync(p => p.Id == id);
        }

        public  async Task UpdatePerson(int id, Person p)
        {
            Person target = await _context.People.FindAsync(id);
            target.FirstName = p.FirstName;
            target.LastName = p.LastName;
            target.Phone = p.Phone;
            target.Email = p.Email;

            await _context.SaveChangesAsync();
        }
        
        public  async Task<IEnumerable<Person>> AllPersons(bool include = true)
        {
            if (include)
            {
                return await _context.People
                    .Include(p => p.Leases)
                        .ThenInclude(apt => apt.Unit)
                            .ThenInclude(unit => unit.ApartmentComplex)
                    .OrderBy(p => p.Current)
                        //.ThenBy(p => p.LastName)
                    .ToListAsync();
            }
            
            return await _context.People.OrderBy(p => p.LastName).ToListAsync();
        }

        public  async Task RemovePerson(int id)
        {
            Person target = new Person() { Id = id };
            _context.Entry(target).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }

        public  async Task InsertLease(Lease l)
        {
            _context.Leases.Add(l);
            await _context.SaveChangesAsync();
        }
        
        public  async Task<Lease> Lease(int id, bool include = true)
        {
            if (include)
            {
                return await _context.Leases
                    .Include(p => p.Tenant)
                    .Include(a => a.Unit)
                        .ThenInclude(a => a.ApartmentComplex)
                    .FirstOrDefaultAsync(e => e.Id == id);
            }

            return await _context.Leases.FirstOrDefaultAsync(e => e.Id == id);
        }
        
        public  async Task<IEnumerable<Lease>> AllLeases(bool include = true)
        {            
            if(include)
            {
                return await _context.Leases
                    .Include(p => p.Tenant)
                    .Include(a => a.Unit)
                        .ThenInclude(e => e.ApartmentComplex)
                    .OrderBy(l => l.Tenant.LastName)
                    .ToListAsync();
            }
            
            return await _context.Leases.ToListAsync();
        }

        public Task UpdateLease(int id, Lease lease)
        {
            throw new NotImplementedException();
        }

        public  async Task RemoveLease(int id)
        {
            Lease target = new Lease() { Id = id };
            _context.Entry(target).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        } 
    }
}