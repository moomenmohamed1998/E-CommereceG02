using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abstraction;
using Microsoft.AspNetCore.Mvc;
using Shared.Dto_s;

namespace Peresentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
   public class ProductControllar(IServicesManager servicesManager):ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetAllProducts()
        {
            var Products=await servicesManager.ProductServices.GetAllProductsAsync();
            return Ok(Products);
        }

        [HttpGet("brands")]
        public async Task<ActionResult<IEnumerable<BrandDto>>> GetAllBrands()
        {
            var Brands = await servicesManager.ProductServices.GetAllBrandsAsync();
            return Ok(Brands);
        }

        [HttpGet("Types")]
        public async Task<ActionResult<IEnumerable<TypeDto>>> GetAllBrandTypes()
        {
            var Types = await servicesManager.ProductServices.GetAllTypesAsync();
            return Ok(Types);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetProductById(int id)
        {
            var Product = await servicesManager.ProductServices.GetProductByIdAsync(id);
            return Ok(Product);
        }

    }
}
