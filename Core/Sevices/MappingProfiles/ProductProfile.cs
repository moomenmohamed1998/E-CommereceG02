using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Models.Products;
using Microsoft.Extensions.Options;
using Shared.Dto_s;

namespace Sevices.MappingProfiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductDto>()
                .ForMember(Dist => Dist.BrandName, Options => Options.MapFrom(src => src.Brand.Name))
                .ForMember(Dist => Dist.TypeName, Options => Options.MapFrom(src => src.Type.Name))
                .ForMember(Dist => Dist.PictureUrl, Options => Options.MapFrom<ProductReslover>());


            CreateMap<ProductBrand, BrandDto>();
            CreateMap<ProductType, TypeDto>();

        }
    }
}
