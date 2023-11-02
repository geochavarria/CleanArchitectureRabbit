using AutoMapper;
using Ecommerce.Application.DTO;
using Ecommerce.Domain.Entities;
using Ecommerce.Domain.Events;

namespace Ecommerce.Application.Common.Mappings
{
    public class MappingsProfile : Profile
    {
        public MappingsProfile()
        {

            CreateMap<Customer, CustomerDto>().ReverseMap();
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<Category, CategoryDTO>().ReverseMap();
            CreateMap<Discount, DiscountDTO>().ReverseMap();
            CreateMap<Discount, DiscountCreatedEvent>().ReverseMap();
            //Por campo
            //CreateMap<Customers, CustomerDto>().ReverseMap()
            //    .ForMember(destination => destination.CustomerId, source => source.MapFrom(src => src.CustomerId))
            //    .ForMember(destination => destination.CompanyName, source => source.MapFrom(src => src.CompanyName));
        }
    }
}