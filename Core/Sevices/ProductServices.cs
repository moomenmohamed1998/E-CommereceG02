﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abstraction;
using AutoMapper;
using Domain.Contracts;
using Domain.Models.Products;
using Sevices.Specifications;
using Shared.Dto_s;

namespace Sevices
{
    public class ProductService(IUnitOfWork unitOfWork,IMapper mapper) : IProductServices
    {
        public async Task<IEnumerable<BrandDto>> GetAllBrandsAsync()
        {
            var _Repository = unitOfWork.GetRepository<ProductBrand, int>();
            var Brands = await _Repository.GetAllAsync();   
            var MappedBrands = mapper.Map<IEnumerable<ProductBrand>,IEnumerable<BrandDto>>(Brands);
            return  MappedBrands;   

        }

        public async Task<IEnumerable<ProductDto>> GetAllProductsAsync()
        {
            var _Repository = unitOfWork.GetRepository<Product, int>();
            var Spec = new ProductWithBrandAndTypeSpecification();
            var Products = await _Repository.GetAllAsync(Spec);
            var MappedProducts = mapper.Map<IEnumerable<Product>, IEnumerable<ProductDto>>(Products);
            return MappedProducts;
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

            var Product = await unitOfWork.GetRepository<Product,int>().GetByIdAsync(id);
            return mapper.Map<Product, ProductDto>(Product);

            //var _Repository = unitOfWork.GetRepository<ProductType, int>();
            //var types = await _Repository.GetAllAsync();
            //var MappedTypes = mapper.Map<IEnumerable<ProductType>, IEnumerable<TypeDto>>(types);
            //return MappedTypes;
        }
    }
}
