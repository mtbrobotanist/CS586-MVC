using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CS586MVC.Data;
using CS586MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CS586MVC.Controllers
{
    public class PropertyDataController : Controller
    {
        private readonly PropertyMismanagementContext _context;
        
        public PropertyDataController(PropertyMismanagementContext context)
        {
            _context = context;
        }
        
        // GET
        public async Task<IEnumerable<Person>> Tenants(int? id)
        {
            if (!id.HasValue)
            {
                return await _context.Person.ToListAsync();
            }

            return await _context.Person.Where(m => m.Id == id).ToListAsync();
        }
        
        // GET
        public async Task<IEnumerable<Complex>> Properties(int? id)
        {
            if (!id.HasValue)
            {
                return await _context.Complex.ToListAsync();
            }

            return await _context.Complex.Where(m => m.Id == id).ToListAsync();
        }
        
        
        // GET
        public async Task<IEnumerable<ComplexUnit>> AllUnits(int? id)
        {
            if (!id.HasValue)
            {
                return await _context.ComplexUnit.ToListAsync();
            }

            return await _context.ComplexUnit.Where(m => m.Id == id).ToListAsync();
        }
        
        // GET
        public async Task<IEnumerable<Lease>> Leases(int? id)
        {
            if (!id.HasValue)
            {
                return await _context.Lease.ToListAsync();
            }

            return await _context.Lease.Where(m => m.Id == id).ToListAsync();
        }
        
    }
}
