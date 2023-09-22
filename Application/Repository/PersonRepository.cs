using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Repository;

public class PersonRepository : GenericRepository<Person>, IPerson
{
    private readonly PharmacyContext _context;
    public PersonRepository(PharmacyContext context) : base(context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Person>> GetSalePatientProduct(string product)
    {
        return await (from p in _context.Products
                        join sp in _context.SaleProducts on p.Id equals sp.Product_Fk
                        join s in _context.Sales on sp.Sale_Fk equals s.Id
                        join pat in _context.People on s.Patient_Fk equals pat.Id
                        where p.Name.ToUpper() == product.ToUpper()
                        select pat).ToListAsync();
    }
}


/* return await (from p in _context.Products 
                    join sp in _context.SaleProducts on p.Id equals sp.Product_Fk
                    where p.Name.ToUpper() == product.ToUpper()
                    select sp).CountAsync(); */