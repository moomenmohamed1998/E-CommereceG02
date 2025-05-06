using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models.Products;
using Shared;

namespace Sevices.Specifications
{
    public class ProductWithBrandAndTypeSpecification : BaseISpecifications<Product, int>
    {
        public ProductWithBrandAndTypeSpecification(ProductQueryParams productQuery)
            : base(p => (!productQuery.BrandId.HasValue || p.BrandId == productQuery.BrandId)
                    && (!productQuery.TypeId.HasValue || p.TypeId == productQuery.TypeId)
            && (string.IsNullOrEmpty(productQuery.SearchValue) || p.Name.ToLower().Contains(productQuery.SearchValue.ToLower())))
        {
            AddInclude(P => P.Brand);
            AddInclude(P => P.Type);
            switch (productQuery.SortingOptions)
            {
                //case ProductSortingOptions.NameAsc:
                //    AddOrderBy(p => p.Name);
                //    break;
                //case ProductSortingOptions.NameDesc:
                //    AddOrderByDescending(p => p.Name);
                //    break;
                //case ProductSortingOptions.PriceAsc:
                //    AddOrderBy(p => p.Price);
                //    break;
                //case ProductSortingOptions.priceDesc:
                //    AddOrderByDescending(p => p.Price);
                //    break;

                //default:
                //    AddOrderBy(p => p.Id);
                //    break;
            }

            ApplyPAgination(productQuery.PageIndex, productQuery.PageSize);

        }
        public ProductWithBrandAndTypeSpecification(int id) : base(p => p.Id == id)
        {
            AddInclude(P => P.Brand);
            AddInclude(P => P.Type);
        }
    }
}
