using ApiPharmacy.Dtos;
using AutoMapper;
using Domain.Entities;

namespace ApiPharmacy.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Role, RoleDto>().ReverseMap();
        CreateMap<PersonType, TypePDto>().ReverseMap();
        CreateMap<JobTitle, JobTitleDto>().ReverseMap();
        CreateMap<IdentificationType, TypeIdenDto>().ReverseMap();
        CreateMap<Email, EmailDto>().ReverseMap();
        
        CreateMap<Person, EmployeeDto>().ReverseMap();
        CreateMap<SalesEmployeeInfo, SalesEmployeDto>().ReverseMap();
        CreateMap<RecipeProduct, ProductRecipeDto>().ReverseMap();
        
        CreateMap<Person, PatientDto>().ReverseMap();
        CreateMap<Person, DoctorDto>().ReverseMap();
        CreateMap<SpentPatient, SpentPatientDto>().ReverseMap(); 


        CreateMap<Recipe, RecipeDto>().ReverseMap();
        CreateMap<Product, ProductNameDto>().ReverseMap();
        CreateMap<Person, SupplierProductDto>().ReverseMap();
        CreateMap<Person, SupplierDto>().ReverseMap();
        CreateMap<Person, PatientRecipeDto>().ReverseMap();

        CreateMap<Person, SupplierContactDto>().ReverseMap();

        CreateMap<ProductMonth,ProductMonthDto>().ReverseMap();
        CreateMap<Product,ProductDto>().ReverseMap();
        CreateMap<Product, ProductSupplierDto>().ReverseMap();

        CreateMap<SupplierGroup, SupplierGroupDto>().ReverseMap();
        CreateMap<TotalProductYear, TotalProductDto>().ReverseMap();
        CreateMap<SupplierGain, SupplierGainDto>().ReverseMap();
        CreateMap<Purchase, PurchaseDto>().ReverseMap();
        CreateMap<Person, SupplierPurchasesDto>().ReverseMap();
        CreateMap<SaleAverange, SaleAverangeDto>().ReverseMap();
        CreateMap<Sale, SaleDto>().ReverseMap();


    }
}
