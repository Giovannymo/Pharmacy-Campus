using Domain.Entities;

namespace Domain.Interfaces;

public interface IPerson : IGenericRepository<Person>
{
    Task<IEnumerable<Person>> GetPurchasePatientProduct(string product);
    Task<IEnumerable<Person>> GetPurchasePatientProductYear(string product, int date);
    Task<IEnumerable<Person>> GetPatientsNeverBuy(int date);
    Task<IEnumerable<SpentPatient>> TotalSpentPatient(int year);
    Task<IEnumerable<Person>> GetAllEmpoyeesAsync();
    Task<IEnumerable<Person>> GetAllPatientAsync();
    Task<IEnumerable<Person>> GetAllSupplierAsync();
    Task<IEnumerable<Person>> GetSaleEmployee(int sales); 
    Task<IEnumerable<Person>> EmployeeNeverSaleMonthYear(int month, int year);
    Task<IEnumerable<Person>> EmployeeNeverSaleYear(int year);
    Task<IEnumerable<Person>> GetEmployeeSaleYear(int sales, int year);
    Task<IEnumerable<SalesEmployeeInfo>> CountAllSalesEmployees(int year);
    Task<IEnumerable<SupplierGain>> TotalSupplierGain(int year);
    Task<IEnumerable<Person>> GetSupplierNeverSell(int year);
    Task<IEnumerable<Person>> GetProductsSoldEachSupplierAsync();
    Task<Person> GetEmployeeDifferentProducts(int year);
    Task<Person> GetPatientSpendMostMoneyInYear(int year);
    Task<IEnumerable<Person>> GetSupplierWithStockAsync(int amount);
    Task<IEnumerable<Person>> GetSuppliersWithAtLeastProductsAsync(int amount, int year);
    Task<Person> GetSupplierMostSuminAsync(int year);
    Task<int> GetTotalSuppliersYearAsync(int year);
    Task<IEnumerable<SupplierGroup>> GetTotalProductsSupplier();
}
