using AutoMapper;
using PoesiaFacil.Entities;
using PoesiaFacil.Models.ViewModels.User;

namespace PoesiaFacil.Mappings
{
    public class UserMapping : Profile
    {
        public UserMapping()
        {
            CreateMap<User, UserViewModel>();
        }
    }
}
