using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models.Products;

namespace Sevices.Specifications
{
   public class ProductWithBrandAndTypeSpecification:BaseISpecifications<Product, int>
    {
        public ProductWithBrandAndTypeSpecification() : base(null)
        {
            AddInclude(P => P.Brand);
            AddInclude(P => P.Type);
        }
    
    }
}
