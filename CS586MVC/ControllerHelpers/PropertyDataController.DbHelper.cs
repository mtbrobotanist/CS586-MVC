using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

using CS586MVC.Data;
using CS586MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace CS586MVC.Controllers
{
    public partial class PropertyDataController
    {
    private static class DbHelper
    {      
        public static PropertyMismanagementContext Context { set; private get; }

        public static async Task<int> InsertAptComplex(AptComplex ac)
        {
            EntityEntry<AptComplex> entry = Context.AptComplexes.Add(ac);
            await Context.SaveChangesAsync();
            return entry.Entity.Id;
        }

        public static async Task<AptComplex> AptComplex(int id, bool include = true)
        {
            if (include)
            {
                return await Context.AptComplexes
                    .Include(e => e.AptComplexUnits)
                        .ThenInclude(e => e.Leases)
                    .FirstOrDefaultAsync(e => e.Id == id);
            }
            
            return await Context.AptComplexes.FirstOrDefaultAsync();
        }
        
        public static async Task<IEnumerable<AptComplex>> AllAptComplexes(bool include = true)
        {
            if (include)
            {
                return await Context.AptComplexes
                    .Include(e => e.AptComplexUnits)
                        .ThenInclude(e => e.Leases)
                    .ToListAsync();
            }
                        
            return await Context.AptComplexes.ToListAsync();
        }

        public static async Task UpdateAptComplex(int id, AptComplex ac)
        {
            AptComplex target = await Context.AptComplexes.FirstAsync(a => a.Id == id);
            target.Address = ac.Address;
            target.Size = ac.Size;
            await Context.SaveChangesAsync();
        }

        public static async Task RemoveAptComplex(int id)
        {
            //shameful
            AptComplex target = await Context.AptComplexes
                .Include(e => e.AptComplexUnits)
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
            foreach(AptComplexUnit unit in target.AptComplexUnits)
            {
                foreach(Lease lease in unit.Leases)
                {
                    Context.Entry(lease.Tenant).State = EntityState.Deleted;
                    Context.Entry(lease).State = EntityState.Deleted;
                }
                Context.Entry(unit).State = EntityState.Deleted;
            }
            
            Context.Entry(target).State = EntityState.Deleted;
            await Context.SaveChangesAsync();
        }

        public static async Task <AptComplexUnit> AptComplexUnit(int id, bool include = true)
        {
            if (include)
            {
                return await Context.AptComplexUnits
                    .Include(e => e.AptComplex)
                    .Include(e => e.AptUnit)
                    .FirstOrDefaultAsync(e => e.Id == id);
            }
            
            return await Context.AptComplexUnits.FirstOrDefaultAsync(e => e.Id == id);
        }
        
        public static async Task<IEnumerable<AptComplexUnit>> AllAptComplexUnits(bool include = true)
        {
            return include ? 
                await Context.AptComplexUnits.Include(e => e.AptComplex).ToListAsync() :    
                await Context.AptComplexUnits.ToListAsync();
        }
        
        public static async Task InsertAptComplexUnit(AptComplexUnit acu)
        {
            bool complex = acu.AptComplex != null;
            bool complexId = acu.AptComplexId.HasValue;
            
            if (!complex && !complexId)
            {
                throw new Exception("an AptComplexUnit object needs an associated AptComplex object!");
            }

            bool unit = acu.AptUnit != null;
            bool hasUnitId = acu.AptUnitId.HasValue;
            
            if (!unit && !hasUnitId)
            {
                throw new Exception("an AptComplexUnit object needs an associated AptUnit object!");
            }

            if (complex)
            {
                int id = await InsertAptComplex(acu.AptComplex);
                acu.AptComplexId = id;
            }

            if (unit)
            {
                int id = await InsertAptUnit(acu.AptUnit);
                acu.AptUnitId = id;
            }
            
            Context.AptComplexUnits.Add(acu);
            await Context.SaveChangesAsync();
        }
    
        public static async Task<int> InsertAptUnit(AptUnit unit)
        {
            EntityEntry<AptUnit> entry = Context.AptUnits.Add(unit);
            await Context.SaveChangesAsync();
            return entry.Entity.Id;
        }
    
        public static async Task InsertPerson(Person p)
        {
            Context.Persons.Add(p);
            await Context.SaveChangesAsync();
        }

        public static async Task<Person> Person(int id, bool include = true)
        {
            if (include)
            {
                return await Context.Persons
                    .Include(p => p.Leases)
                        .ThenInclude(apt => apt.Unit)
                            .ThenInclude(unit => unit.AptComplex)
                    .FirstOrDefaultAsync(p => p.Id == id);
            }

            return await Context.Persons.FirstOrDefaultAsync(p => p.Id == id);
        }
        
        public static async Task<IEnumerable<Person>> AllPersons(bool include = true)
        {
            if (include)
            {
                return await Context.Persons
                    .Include(p => p.Leases)
                        .ThenInclude(apt => apt.Unit)
                            .ThenInclude(unit => unit.AptComplex)
                    .ToListAsync();
            }
            
            return await Context.Persons.ToListAsync();
        }
        
        public static async Task InsertLease(Lease l)
        {
            Context.Leases.Add(l);
            await Context.SaveChangesAsync();
        }
        public static async Task<Lease> Lease(int id, bool include = true)
        {
            if (include)
            {
                return await Context.Leases
                    .Include(p => p.Tenant)
                    .Include(a => a.Unit)
                        .ThenInclude(a => a.AptComplex)
                    .FirstOrDefaultAsync(e => e.Id == id);
            }

            return await Context.Leases.FirstOrDefaultAsync(e => e.Id == id);
        }
        
        public static async Task<IEnumerable<Lease>> AllLeases(bool include = true)
        {            
            if(include)
            {
                return await Context.Leases
                    .Include(p => p.Tenant)
                    .Include(a => a.Unit)
                        .ThenInclude(e => e.AptComplex)
                    .ToListAsync();
            }
            
            return await Context.Leases.ToListAsync();
        }
        
    } 
    }
}