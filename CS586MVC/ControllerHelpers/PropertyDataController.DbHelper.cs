using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

using CS586MVC.Data;
using CS586MVC.Models;

namespace CS586MVC.Controllers
 {
     public partial class PropertyDataController
     {
         private static class DbHelper
         {      
             public static PropertyMismanagementContext Context { set; private get; }
    
             public static async Task<AptComplex> AptComplex(int id, bool include = true)
             {
                 return include ? 
                     await Context.AptComplex.Include(e => e.OccupiedUnits).FirstOrDefaultAsync(e => e.Id == id) : 
                     await Context.AptComplex.FirstOrDefaultAsync();
             }
             
             public static async Task<IEnumerable<AptComplex>> AllAptComplexes(bool include = true)
             {
                return include ? 
                    await Context.AptComplex.Include(e => e.OccupiedUnits).ToListAsync() :    
                    await Context.AptComplex.ToListAsync();
             }
    
             public static async Task <AptComplexUnit> AptComplexUnit(int id, bool include = true)
             {
                 return include ?
                     await Context.AptComplexUnit.Include(e => e.AptComplex).Include(e => e.AptUnit).FirstOrDefaultAsync(e => e.Id == id) :
                     await Context.AptComplexUnit.FirstOrDefaultAsync(e => e.Id == id);
             }
             
             public static async Task<IEnumerable<AptComplexUnit>> AllAptComplexUnits(bool include = true)
             {
                 return include ? 
                     await Context.AptComplexUnit.Include(e => e.AptComplex).ToListAsync() :    
                     await Context.AptComplexUnit.ToListAsync();
             }
             
             public static async Task<Lease> Lease(int id, bool include = true)
             {
                 if (include)
                 {
                     Context.Lease.Where(l => l.Id == id).Load();
                 }

                 return await Context.Lease.FirstOrDefaultAsync(e => e.Id == id);
             }
             
             public static async Task<IEnumerable<Lease>> AllLeases(bool include = true)
             {
                 if (include)
                 {
                     Context.Lease.Load();
                 }
                  
                 return await Context.Lease.ToListAsync();
             }
             
             public static async Task<Person> Person(int id, bool include = true)
             {
                 if (include)
                 {
                     await Context.Person.Where(l => l.Id == id).LoadAsync();
                 }

                 return await Context.Person.FirstOrDefaultAsync(e => e.Id == id);
             }
             
             public static async Task<IEnumerable<Person>> AllPersons(bool include = true)
             {
                 if (include)
                 {
                     Context.Person.Load();
                 }
                  
                 return await Context.Person.ToListAsync();
             }
         } 
     }
 }