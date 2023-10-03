using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Repository;
public class RecipeRepository : GenericRepository<Recipe>, IRecipe
{
    private readonly PharmacyContext _context;
    public RecipeRepository(PharmacyContext context) : base(context)
    {
        _context = context;
    }

    //Obtener recetas médicas emitidas después del dia (X) del mes (X) del año (X)
    public async Task<IEnumerable<Recipe>> GetRecordsByDate(DateTime date)
    {
        return await _context.Recipes
                    .Where(e => e.CreateDate.Date >= date.Date)
                    .Include(e => e.RecipeProducts).ThenInclude(rp => rp.Product)
                    .Include(e => e.Doctor)
                    .Include(e => e.Patient)
                    .ThenInclude(p => p.IdentificationType)
                    .ToListAsync();
    }
}