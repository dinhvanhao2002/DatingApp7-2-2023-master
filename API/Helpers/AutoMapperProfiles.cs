using System.Linq;
using API.DTOs;
using API.Entities;
using API.Extensions;
using AutoMapper;

namespace API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<AppUser, MemberDto>()  // thuộc tính appuser đc ánh xạ vào các thuộc tính của memberdto
            .ForMember(dest =>dest.PhotoUrl, opt => opt.MapFrom( src=>
             src.Photos.FirstOrDefault(x=>x.IsMain).Url))
             .ForMember(dest => dest.Age, opt => opt.MapFrom(src =>
             src.DateOfBirth.CalculateAge()));
            CreateMap<Photo, PhotoDto>(); // cho phép dữ liệu ảnh đc chuyển đổi từ lớp photo sang lớp photodto
            CreateMap<MemberUpdateDto, AppUser>();
            
               
        }
    }
}


