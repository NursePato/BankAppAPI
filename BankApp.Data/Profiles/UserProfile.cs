using AutoMapper;
using BankApp.Data.DTO;
using BankApp.Domain.Models;

namespace BankApp.Data.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            
            CreateMap<CreateCustomerDto, Customer>()
                .ForMember(dest => dest.CustomerId, opt => opt.Ignore())
                .ForMember(dest => dest.Streetaddress, opt => opt.MapFrom(src => src.StreetAddress)) 
                .ForMember(dest => dest.Zipcode, opt => opt.MapFrom(src => src.ZipCode))
                .ForMember(dest => dest.Givenname, opt => opt.MapFrom(src => src.GivenName));

            
            CreateMap<CreateCustomerDto, Account>()
                .ForMember(dest => dest.Frequency, opt => opt.MapFrom(src => "Monthly"))
                .ForMember(dest => dest.Created, opt => opt.MapFrom(src => DateTime.UtcNow)) 
                .ForMember(dest => dest.Balance, opt => opt.MapFrom(src => src.InitialDeposit)); 

            
            CreateMap<User, UserDto>()
                .ForMember(dest => dest.Token, opt => opt.Ignore());
        }
    }
}
