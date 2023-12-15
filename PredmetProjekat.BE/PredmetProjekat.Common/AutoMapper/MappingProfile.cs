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
                .ForMember(dest => dest.Product, opt => opt.MapFrom(src => new Product { ProductId = src.ProductId }))
                .ReverseMap();
            CreateMap<Register, RegisterDtoId>().ReverseMap();
            CreateMap<Account, RegistrationDto>().ReverseMap();
            CreateMap<Account, UserDto>().ReverseMap();
            CreateMap<Account, EmployeeDto>().ReverseMap();
            CreateMap<Receipt, ReceiptDto>().ReverseMap();

        }
    }
}
