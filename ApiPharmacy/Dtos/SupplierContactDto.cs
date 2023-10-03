using System;
namespace ApiPharmacy.Dtos;

public class SupplierContactDto
{
    public string Name { get; set; }
    public List<EmailDto> Emails {get; set; }

}
