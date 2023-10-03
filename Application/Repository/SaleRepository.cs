using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Repository;

public class SaleRepository : GenericRepository<Sale>, ISale
{
    private readonly PharmacyContext _context;
    public SaleRepository(PharmacyContext context) : base(context)
    {
        _context = context;
    }

    //Total de ventas del medicamento (X) (OK)
    public async Task<decimal> GetTotalSaleProduct(string product)
    {
        var saleProducts = await _context.SaleProducts
        .Where(sp => sp.Product.Name.ToUpper() == product.ToUpper())
        .ToListAsync();

        // Calcular el total de ventas sumando la cantidad vendida por producto
        decimal totalSales = saleProducts.Sum(sp => sp.Price);

        return totalSales;
    }
}