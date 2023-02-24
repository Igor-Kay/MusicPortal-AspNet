using AutoMapper;
using MusicPortal.BLL.DTO;
using MusicPortal.DAL.Models;
using MusicPortal.WEB.Models.ViewModels;

namespace MusicPortal.WEB.AutoMapperProfiles
{
    public class HuetaProfile : Profile
    {
        public HuetaProfile() { 
            CreateMap<Music, MusicDTO>().ReverseMap();
            CreateMap<MusicDTO, MusicVM>().ReverseMap();
        }
    }
}
