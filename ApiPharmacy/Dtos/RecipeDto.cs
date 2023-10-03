namespace ApiPharmacy.Dtos;

public class RecipeDto
{
    public int Id { get; set; }
    public DateTime CreateDate { get; set; }
    public DoctorDto Doctor { get; set; }
    public PatientRecipeDto Patient { get; set; }
    public List<ProductRecipeDto> RecipeProducts { get; set; }
}
