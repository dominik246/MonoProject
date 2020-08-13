using AutoMapper;

using Project.MVC.Models;
using Project.Service.Models;

namespace Project.MVC
{
    public class VehicleProfile : Profile
    {
        public VehicleProfile()
        {
            CreateMap<VehicleMake, VehicleMakeDTO>();
            CreateMap<VehicleModel, VehicleModelDTO>();
            CreateMap(typeof(PageModel<>), typeof(PageModelDTO<>));
            CreateMap<VehicleModelDTO, VehicleModel>();
            CreateMap<VehicleMakeDTO, VehicleMake>();
        }
    }
}
