using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiPharmacy.Dtos
{
    public class SaleDto
    {
        
    public DateTime SaleDate { get; set; }
    public PatientDto Patient { get; set; }
    public EmployeeDto Employee { get; set; }
    }
}