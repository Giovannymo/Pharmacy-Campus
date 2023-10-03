using System.Text.Json.Serialization;

namespace ApiPharmacy.Dtos;

public class DoctorDto
{   
    [JsonIgnore]
    public int Id { get; set; }
    public string Name { get; set; }
    public string Identification { get; set; }
}
