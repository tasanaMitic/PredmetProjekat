using AutoMapper;
using PredmetProjekat.Common.Dtos;
using PredmetProjekat.Common.Dtos.IdentityDtos;
using PredmetProjekat.Common.Dtos.ProductDtos;
using PredmetProjekat.Common.Dtos.UserDtos;
using PredmetProjekat.Models.Models;

namespace PredmetProjekat.Common.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Brand, BrandDtoId>().ReverseMap();
            CreateMap<Category, CategoryDtoId>().ReverseMap();
            CreateMap<Product, StockedProductDto>().ReverseMap();
            CreateMap<SoldProductDto, SoldProduct>()
                .ForMember(dest => dest.SoldProductId, opt => opt.MapFrom(src => src.SoldProductId == Guid.Empty ? Guid.NewGuid() : src.SoldProductId))
                .ReverseMap();
            CreateMap<AttributeValue, AttributeValueDto>()
                .ForMember(dest => dest.AttributeId, opt => opt.MapFrom(src => src.ProductAttribute.AttributeId))
                .ForMember(dest => dest.AttributeValue, opt => opt.MapFrom(src => src.Value))
                .ReverseMap();

            CreateMap<AttributeValue, AttributeValueDto>()
                .ForMember(dest => dest.AttributeName, opt => opt.MapFrom(src => src.ProductAttribute.AttributeName))
                .ForMember(dest => dest.AttributeValue, opt => opt.MapFrom(src => src.Value))
                .ForMember(dest => dest.AttributeId, opt => opt.MapFrom(src => src.ProductAttributeId));
            CreateMap<AttributeDto, ProductAttribute>().ReverseMap();
            CreateMap<Register, RegisterDtoId>().ReverseMap();
            CreateMap<ProductType, ProductTypeDtoId>().ReverseMap();
            CreateMap<Account, RegistrationDto>().ReverseMap();
            CreateMap<Account, UserDto>().ReverseMap();
            CreateMap<Account, EmployeeDto>().ReverseMap();
            CreateMap<Receipt, ReceiptDto>().ReverseMap();

        }
    }
}
