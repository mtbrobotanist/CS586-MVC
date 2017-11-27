using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

using CS586MVC.Data;
using CS586MVC.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.WindowsAzure.Storage.Blob;

namespace CS586MVC.Services
{
    public class PropertyMismanagementDatabaseService : IDatabaseService
    {
        private PropertyMismanagementContext _context;
        
        public PropertyMismanagementDatabaseService(PropertyMismanagementContext context)
        {
            this._context = context;
        }

        public async Task<int> InsertApartmentComplex(ApartmentComplex ac)
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

        public async Task<ApartmentComplex> ApartmentComplex(int id, bool include = true)
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
        
        public async Task<IEnumerable<ApartmentComplex>> AllApartmentComplexes(bool include = true)
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

        public async Task UpdateApartmentComplex(int id, ApartmentComplex ac)
        {
            ApartmentComplex target = await _context.ApartmentComplexes.FirstAsync(a => a.Id == id);
            target.Address = ac.Address;
            target.Size = ac.Size;
            await _context.SaveChangesAsync();
        }

        public async Task RemoveApartmentComplex(int id)
        {
            //shameful
            ApartmentComplex target = await _context.ApartmentComplexes
                .Include(e => e.ApartmentComplexUnits)
                    .ThenInclude(e => e.Leases)
                        .ThenInclude(e => e.Person)
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
                    _context.Entry(lease.Person).State = EntityState.Deleted;
                    _context.Entry(lease).State = EntityState.Deleted;
                }
                _context.Entry(unit).State = EntityState.Deleted;
            }
            
            _context.Entry(target).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }

        public async Task InsertApartmentComplexUnit(ApartmentComplexUnit acu)
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
        
        public async Task <ApartmentComplexUnit> ApartmentComplexUnit(int id, bool include = true)
        {
            if (include)
            {
                return await _context.ApartmentComplexUnits
                    .Include(e => e.ApartmentComplex)
                    .FirstOrDefaultAsync(e => e.Id == id);
            }
            
            return await _context.ApartmentComplexUnits.FirstOrDefaultAsync(e => e.Id == id);
        }
        
        public async Task<IEnumerable<ApartmentComplexUnit>> AllApartmentComplexUnits(bool include = true)
        {
            return include ? 
                await _context.ApartmentComplexUnits.Include(e => e.ApartmentComplex).ToListAsync() :    
                await _context.ApartmentComplexUnits.ToListAsync();
        }

        public async Task UpdateApartmentComplexUnit(int id, ApartmentComplexUnit acu)
        {
            ApartmentComplexUnit target = await _context.ApartmentComplexUnits
                .Include(a => a.ApartmentComplex).FirstAsync(a => a.Id == id);

            target.BedRooms = acu.BedRooms;
            target.BathRooms = acu.BathRooms;
            target.Area = acu.Area;

            if (acu.ApartmentComplex != null)
            {
                target.ApartmentComplex = acu.ApartmentComplex;
            }
            else if (acu.ApartmentComplexId.HasValue)
            {
                target.ApartmentComplexId = acu.ApartmentComplexId.Value;
            }

            await _context.SaveChangesAsync();
        }

        public async Task RemoveApartmentComplexUnit(int id)
        {
            ApartmentComplexUnit acu = new ApartmentComplexUnit() {Id = id};
            _context.Entry(acu).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }
    
        public async Task<int> InsertPerson(Person p)
        {
            Person previous = await _context.People.FirstOrDefaultAsync(person => person.Equals(p));

            if (previous != null)
            {
                return previous.Id;
            }
            
            EntityEntry<Person> entry = _context.People.Add(p);
            await _context.SaveChangesAsync();

            return entry.Entity.Id;
        }

        public async Task<Person> Person(int id, bool include = true)
        {
            if (include)
            {
                return await _context.People
                    .Include(p => p.Leases)
                        .ThenInclude(apt => apt.ApartmentComplexUnit)
                            .ThenInclude(unit => unit.ApartmentComplex)
                    .FirstOrDefaultAsync(p => p.Id == id);
            }

            return await _context.People.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task UpdatePerson(int id, Person p)
        {
            Person target = await _context.People.FindAsync(id);
            target.FirstName = p.FirstName;
            target.LastName = p.LastName;
            target.Phone = p.Phone;
            target.Email = p.Email;

            await _context.SaveChangesAsync();
        }
        
        public async Task<IEnumerable<Person>> AllPersons(bool include = true)
        {
            if (include)
            {
                return await _context.People
                    .Include(p => p.Leases)
                        .ThenInclude(apt => apt.ApartmentComplexUnit)
                            .ThenInclude(unit => unit.ApartmentComplex)
                    .OrderBy(p => p.Current)
                        //.ThenBy(p => p.LastName)
                    .ToListAsync();
            }
            
            return await _context.People.OrderBy(p => p.LastName).ToListAsync();
        }

        public async Task RemovePerson(int id)
        {
            Person target = new Person() { Id = id };
            _context.Entry(target).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }

        public async Task InsertLease(Lease l)
        {
            _context.Leases.Add(l);
            await _context.SaveChangesAsync();
        }
        
        public async Task<Lease> Lease(int id, bool include = true)
        {
            if (include)
            {
                return await _context.Leases
                    .Include(p => p.Person)
                    .Include(a => a.ApartmentComplexUnit)
                        .ThenInclude(a => a.ApartmentComplex)
                    .FirstOrDefaultAsync(e => e.Id == id);
            }

            return await _context.Leases.FirstOrDefaultAsync(e => e.Id == id);
        }
        
        public async Task<IEnumerable<Lease>> AllLeases(bool include = true)
        {            
            if(include)
            {
                return await _context.Leases
                    .Include(p => p.Person)
                    .Include(a => a.ApartmentComplexUnit)
                        .ThenInclude(e => e.ApartmentComplex)
                    .OrderBy(l => l.Person.LastName)
                    .ToListAsync();
            }
            
            return await _context.Leases.ToListAsync();
        }

        public async Task UpdateLease(int id, Lease lease)
        {
            Lease target = await Lease(id);
            if (target == null)
            {
                return;
            }

            target.StartDate = lease.StartDate;
            target.DurationMonths = lease.DurationMonths;
            target.RentMonthly = lease.RentMonthly;

            if (lease.ApartmentComplexUnitId != 0 
                && lease.ApartmentComplexUnitId != target.ApartmentComplexUnitId)
            {
                ApartmentComplexUnit acu = await ApartmentComplexUnit(lease.ApartmentComplexUnitId);
                target.ApartmentComplexUnit = acu;
                target.ApartmentComplexUnitId = lease.ApartmentComplexUnitId;
            }
            else if (lease.ApartmentComplexUnit != null)
            {
                // this could throw an error if we are putting in an apartmentcomplexunit that doesn't already exist in the db
                target.ApartmentComplexUnit.UnitNumber = lease.ApartmentComplexUnit.UnitNumber;
            }

            await _context.SaveChangesAsync();
        }

        public async Task RemoveLease(int id)
        {
            Lease target = new Lease() { Id = id };
            _context.Entry(target).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        } 
    }
}