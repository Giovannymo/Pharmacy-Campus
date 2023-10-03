
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence;
namespace Application.Repository;

public class ProductRepository : GenericRepository<Product>, IProduct
{
    private readonly PharmacyContext _context;
    public ProductRepository(PharmacyContext context) : base(context)
    {
        _context = context;
    }

    //Listar los proveedores con su información de contacto en medicamentos. (OK)
    public async Task<IEnumerable<Product>> GetAllInfoAsync()
    {
        return await _context.Products
                        .Include(p => p.Supplier)
                        .ThenInclude(s => s.Emails)
                        .Where(p => p.Supplier != null)
                        .ToListAsync();
    }

    //Total de medicamentos vendidos en el trimestre (X) del Año (X). (OK) 
    public async Task<IEnumerable<TotalProductYear>> AllSalesQuarter(int year, int trim)
    {
        int month = (trim - 1) * 3 + 1;
        
        return await _context.SaleProducts
        .Where(sp => sp.Sale.SaleDate.Year == year && sp.Sale.SaleDate.Month >= month 
        && sp.Sale.SaleDate.Month <= month + 2)
        .GroupBy(sp => sp.Product.Name) 
        .Select(group => new TotalProductYear
        {
            Product = group.Key,
            Quantity = group.Sum(sp => sp.Quantity)
        })
        .ToListAsync();
    }


    //Cantidad total de dinero recaudado por las ventas de medicamentos (OK)
    public async Task<decimal> GetTotalGain()
    {
        return await _context.SaleProducts.SumAsync(sp => sp.Price);

    }

    //Obtener todos los medicamentos con menos de (X) unidades en stock (OK)
    public async Task<IEnumerable<Product>> GetLessThanStockAsync(int amount)
    {
        return await _context.Products
                            .Where(p => p.Stock < amount)
                            .ToListAsync();
    }

    //Medicamentos que caducan antes del dia (X) del Mes (X) del año (X) (OK)
    public async Task<IEnumerable<Product>> GetAllProductExpiredBeforeAsync(DateTime expiryDate)
    {
        return await _context.Products
                        .Where(p => p.ExpirationDate.Date < expiryDate)
                        .ToListAsync();
    }

    //Medicamentos con un precio mayor a (X) y un stock menor a (X). (OK)
    public async Task<IEnumerable<Product>> GetHighPricedLowStockAsync(decimal price, double stock)
    {
        return await _context.Products
                        .Where(p => p.Price > price && p.Stock < stock)
                        .ToListAsync();
    } 

    //Medicamentos que no han sido vendidos nunca. (OK)
    public async Task<IEnumerable<Product>> GetAllProductsNeverSold()
    {
        return await _context.Products
                            .Where(p => !p.SaleProducts.Any())
                            .ToListAsync();
    }

    //Medicamentos comprados al ‘Proveedor A’. (OK)
    public async Task<IEnumerable<Product>> GetAllProductsBySupplierAsync(string supplier)
    {
        return await _context.Products
                            .Where(product =>
                            product.PurchaseProducts.Any(purchaseProduct =>
                            purchaseProduct.Purchase.Supplier.Name.ToUpper() == supplier.ToUpper() ))
                            .ToListAsync();
    } 

    //Medicamentos que no han sido vendidos en el año (X). (OK)
    public async Task<IEnumerable<Product>> GetAllProductsNotSoldInYearAsync(int year)
    {
        return await _context.Products
                            .Where(product =>
                            !product.SaleProducts.Any(sp => sp.Sale.SaleDate.Year >= (year - 1) 
                            && sp.Sale.SaleDate.Year <= year))
                            .ToListAsync();
    }  
   
    //Medicamentos que han sido vendidos cada mes del año 2023 (OK)
    public async Task<IEnumerable<Product>> GetAllProductsSoldInMonthAsync(int year)
    {
        return await _context.Products
                        .Where(product =>product.SaleProducts
                        .All(sp => sp.Sale.SaleDate.Year == year))
                        .ToListAsync();
    }

    //Obtener el medicamento menos vendido en 2023. (OK)
    public async Task<Product> GetLowestSellingProductAsync(int year)
    {
        return await _context.Products
            .Where(p => p.SaleProducts.Any(sp => sp.Sale.SaleDate.Year == year ))
            .OrderBy(p => p.SaleProducts.Sum(p => p.Quantity)) // Ordenar por la suma de las cantidades vendidas
            .FirstAsync(); 
    }

    //Obtener el medicamento más caro.(OK)
    public async Task<Product> GetProductMostExpensive()
    {
        return await _context.Products
                .OrderByDescending(p => p.Price)
                .FirstAsync();
    }

    //Obtener el total de medicamentos vendidos en marzo de 2023.(OK)
    public async Task<int> GetTotalProductMonthAsync(int month, int year)
    {
        return await _context.SaleProducts
            .Where(sp => sp.Sale.SaleDate.Year == year && sp.Sale.SaleDate.Month == month)
            .SumAsync(sp => sp.Quantity); 
    }

    //Obtener todos los medicamentos que expiren en 2024.(OK)
    public async Task<IEnumerable<Product>> GetAllProductExpiredAsync(int year)
    {
        return await _context.Products
                        .Where(p => p.ExpirationDate.Date.Year == year)
                        .ToListAsync();
    }

    //Total de medicamentos vendidos por mes en 2023 (OK)
    public async Task<IEnumerable<ProductMonth>> GetTotalProduct(int year)
    {
        return await _context.Sales
            .Where(sale => sale.SaleDate.Year == year)
            .GroupBy(sale => sale.SaleDate.Month)
            .Select(group => new ProductMonth
            {
                Month = group.Key,
                TotalMedicationsSold = group.Sum(sale => sale.SaleProducts.Sum(sp => sp.Quantity))
            })
            .OrderBy(result => result.Month) 
            .ToListAsync();
    }

        //Promedio de medicamentos comprados por venta.
        public async Task<IEnumerable<SaleAverange>> GetProductsAverangebySaleAsync()
        {
            return await _context.SaleProducts
                .GroupBy(p => p.Sale_Fk)
                .Select(g => new SaleAverange {
                    Sale = g.First().Sale.Id,
                    Averange = g.Average(p => p.Quantity)
                })
                .ToListAsync();
                                                      
        }

}   