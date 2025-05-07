using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abstraction;
using AutoMapper;
using Domain.Contracts;
using Domain.Exceptions;
using Domain.Models.Products;
using Sevices.Specifications;
using Shared;
using Shared.Dto_s;

namespace Sevices
{
    public class ProductService(IUnitOfWork unitOfWork, IMapper mapper) : IProductServices
    {
        public async Task<IEnumerable<BrandDto>> GetAllBrandsAsync()
        {
            var _Repository = unitOfWork.GetRepository<ProductBrand, int>();
            var Brands = await _Repository.GetAllAsync();
            var MappedBrands = mapper.Map<IEnumerable<ProductBrand>, IEnumerable<BrandDto>>(Brands);
            return MappedBrands;

        }

        public async Task<PaginateResult<ProductDto>> GetAllProductsAsync(ProductQueryParams productQuery)
        {
            var _Repository = unitOfWork.GetRepository<Product, int>();
            var Spec = new ProductWithBrandAndTypeSpecification(productQuery);
            var Products = await _Repository.GetAllAsync(Spec);
            var MappedProducts = mapper.Map<IEnumerable<Product>, IEnumerable<ProductDto>>(Products);

            var countedProducts = Products.Count();
            var CountSpec = new ProductWithBrandAndTypeSpecification(productQuery);
            var TotalCount = await _Repository.CountAsync(CountSpec);
            return new PaginateResult<ProductDto>(productQuery.PageIndex, countedProducts, TotalCount, MappedProducts);
            //return MappedProducts;
        }

        public Task<IEnumerable<ProductDto>> GetAllProductsAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<TypeDto>> GetAllTypesAsync()
        {
            var _Repository = unitOfWork.GetRepository<ProductType, int>();
            var types = await _Repository.GetAllAsync();
            var MappedTypes = mapper.Map<IEnumerable<ProductType>, IEnumerable<TypeDto>>(types);
            return MappedTypes;
        }

        public async Task<ProductDto> GetProductByIdAsync(int id)
        {
            var Spec = new ProductWithBrandAndTypeSpecification(id);

            var Product = await unitOfWork.GetRepository<Product, int>().GetByAsynce(Spec);
            if (Product is null)

                throw new ProductNotFoundExceptions(id);

            return mapper.Map<Product, ProductDto>(Product);


        }
    }
}
