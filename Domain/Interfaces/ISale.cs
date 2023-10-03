using Domain.Entities;

namespace Domain.Interfaces;

public interface ISale : IGenericRepository<Sale>
{
  Task<decimal> GetTotalSaleProduct(string product);
}
