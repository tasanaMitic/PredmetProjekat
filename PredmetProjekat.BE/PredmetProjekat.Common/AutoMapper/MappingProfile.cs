using AutoMapper;
using PredmetProjekat.Common.Dtos;
using PredmetProjekat.Models.Models;

namespace PredmetProjekat.Common.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile() //TODO
        {
            CreateMap<Brand, BrandDtoId>().ReverseMap();
            CreateMap<Category, CategoryDtoId>().ReverseMap();
            CreateMap<Product, ProductDtoId>().ReverseMap();
            CreateMap<Register, RegisterDtoId>().ReverseMap();
            CreateMap<Account, AccountDto>().ReverseMap();
        }
    }
}
