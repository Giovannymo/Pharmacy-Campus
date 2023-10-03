using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ProductsSoldEachSupplier
    {
        public string SupplierName { get; set; }
        public ICollection<PurchaseProduct> PurchaseProducts{ get; set; }   
    }
}