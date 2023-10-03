using System.Xml.Serialization;

namespace Domain.Entities;

public class IdentificationType : BaseEntity
{
    public string Description { get; set; }
    [XmlIgnore]
    public ICollection<Person> People { get; set; }
}
