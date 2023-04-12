using AutoMapper;
using WebApi.Entities;
using WebApi.Models.Users;

namespace WebApi.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // User -> AuthenticateResponse
            CreateMap<User, AuthenticateResponse>();
            // RegisterRequest -> User
            CreateMap<RegisterRequest, User>();
            // Location.RegisterRequest -> Location
            CreateMap<WebApi.Models.Locations.RegisterRequest, Location>();
            // Location.UpdateRequest -> Location
            CreateMap<WebApi.Models.Locations.UpdateRequest, Location>();
            // Machine.RegisterRequest -> Machine
            CreateMap<WebApi.Models.Machines.RegisterRequest, Machine>();
            // Build.RegisterRequest -> Build
            CreateMap<WebApi.Models.Builds.RegisterRequest, Build>();

            CreateMap<WebApi.Models.MachineType.RegisterRequest, MachineType>();
            // Room.RegisterRequest -> Room
            CreateMap<WebApi.Models.Rooms.RegisterRequest, Room>();
            // Machines.UpdateRequest -> Machine
            CreateMap<WebApi.Models.Machines.UpdateRequest, Machine>();
            // UpdateRequest -> User
            CreateMap<UpdateRequest, User>()
                .ForAllMembers(x => x.Condition(
                    (src, dest, prop) =>
                    {
                        // ignore null & empty string properties
                        if (prop == null) return false;
                        if (prop.GetType() == typeof(string) && string.IsNullOrEmpty((string)prop)) return false;

                        return true;
                    }
                ));
        }
    }
}