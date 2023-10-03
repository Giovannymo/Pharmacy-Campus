namespace ApiPharmacy.Dtos;

public class PatientRecipeDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Identification { get; set; }
    public TypeIdenDto IdentificationType { get; set; }
    
}
