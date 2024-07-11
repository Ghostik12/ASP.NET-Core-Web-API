using AutoMapper;
using WebApiVS.Configuration;
using HomeApi.Contracts.Models.Home;

namespace WebApiVS
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<Address, AddressInfo>();
            CreateMap<HomeOptions, InfoResponse>()
                .ForMember(mbox =>  mbox.AddressInfo, opt =>  opt.MapFrom(src => src.Address));
        }
    }
}
