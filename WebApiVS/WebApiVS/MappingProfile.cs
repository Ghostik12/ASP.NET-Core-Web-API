using AutoMapper;
using WebApiVS.Configuration;
using HomeApi.Contracts.Models.Home;
using HomeApi.Contracts.Models.Devices;
using HomeApi.Contracts.Models.Rooms;
using HomeApi.Data.Models;

namespace WebApiVS
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<Address, AddressInfo>();
            CreateMap<HomeOptions, InfoResponse>()
                .ForMember(mbox =>  mbox.AddressInfo, opt =>  opt.MapFrom(src => src.Address));

            // Валидация запросов:
            CreateMap<AddDeviceRequest, Device>();
            CreateMap<AddRoomRequest, Room>();
            CreateMap<Device, DeviceView>();
        }
    }
}
