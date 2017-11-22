using System.Collections.Generic;
using System.Threading.Tasks;
using CS586MVC.Models;

namespace CS586MVC.Services
{
    public interface IDatabaseService
    {
        Task<int> InsertApartmentComplex(ApartmentComplex apartmentComplex);
        Task<ApartmentComplex> ApartmentComplex(int id, bool include = true);
        Task<IEnumerable<ApartmentComplex>> AllApartmentComplexes(bool include = true);
        Task UpdateApartmentComplex(int id, ApartmentComplex ac);
        Task RemoveApartmentComplex(int id);
        
        Task InsertApartmentComplexUnit(ApartmentComplexUnit acu);
        Task <ApartmentComplexUnit> ApartmentComplexUnit(int id, bool include = true);
        Task<IEnumerable<ApartmentComplexUnit>> AllApartmentComplexUnits(bool include = true);
        Task UpdateApartmentComplexUnit(int id, ApartmentComplexUnit acu);
        Task RemoveApartmentComplexUnit(int id);
        
        Task InsertPerson(Person p);
        Task<Person> Person(int id, bool include = true);
        Task UpdatePerson(int id, Person p);
        Task<IEnumerable<Person>> AllPersons(bool include = true);
        Task RemovePerson(int id);
        
        Task InsertLease(Lease lease);
        Task<Lease> Lease(int id, bool include = true);
        Task<IEnumerable<Lease>> AllLeases(bool include = true);
        Task UpdateLease(int id, Lease lease);
        Task RemoveLease(int id);
    }
}