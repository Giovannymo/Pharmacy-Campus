using Domain.Entities;

namespace Domain.Interfaces;

public interface IPerson : IGenericRepository<Person>
{
    Task<IEnumerable<Person>> GetSalePatientProduct(string product);

    Task<IEnumerable<Person>> GetSalePatientProductYear(string product, int date);
    Task<IEnumerable<Person>> GetSaleEmployee(int sales); 

    Task<IEnumerable<Person>> GetSuppliersNoSalesAtYear();
}
