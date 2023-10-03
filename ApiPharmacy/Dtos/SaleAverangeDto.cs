using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;

namespace ApiPharmacy.Dtos
{
    public class SaleAverangeDto
    {
        public int Sale { get; set; }
        public double Averange  {get;set;}
    }
}