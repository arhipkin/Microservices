using AutoMapper;
using Core.Models.Entities;
using Core.ViewModels;

namespace WebApi.Configuration.AutoMapperProfiles
{
    public class DefaultProfile : Profile
    {
        public DefaultProfile()
        {
            CreateMap<Core.Models.Entities.UserDetails, Core.ViewModels.UserDetails>().ReverseMap();
            CreateMap<AppUser, User>()
                .ForMember(dest => dest.Roles, opt => opt.MapFrom(src => src.UserRoles));
            CreateMap<User, AppUser>();
            CreateMap<AppRole, Role>();

            CreateMap<ICollection<AppUserRole>, IEnumerable<Role>>()
                .ConvertUsing(src => 
                    src.Select(x => new Role { Id = x.Role.Id, Name = x.Role.Name, NormalizedName = x.Role.NormalizedName })
                );

            CreateMap(typeof(Core.Models.ResultCommand<>), typeof(ResultResponse<>));
        }
    }
}
