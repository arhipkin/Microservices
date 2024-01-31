using AutoMapper;
using Core.Models.Entities;

namespace WebApi.Configuration.AutoMapperProfiles
{
    public class DefaultProfile : Profile
    {
        public DefaultProfile()
        {
            CreateMap<Core.Models.Entities.UserDetails, Core.ViewModels.UserDetails>().ReverseMap();
            CreateMap<Core.Models.Entities.AppUser, Core.ViewModels.User>().ReverseMap();
            CreateMap<Core.Models.Entities.AppRole, Core.ViewModels.Role>().ReverseMap();
            CreateMap(typeof(Core.Models.ResultCommand<>), typeof(Core.ViewModels.ResultResponse<>)).ReverseMap();
        }
    }
}
