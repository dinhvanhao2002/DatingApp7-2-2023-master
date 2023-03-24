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
            CreateMap<RegisterDTO, AppUser>();
            CreateMap<Message, MessageDto>()  //cho phép sao chép dữ liệu từ đối tượng mesage sang messagedto 
               .ForMember(dest => dest.SenderPhotoUrl, opt => opt.MapFrom(src => 
               src.Sender.Photos.FirstOrDefault(x=> x.IsMain).Url))
               .ForMember(dest => dest.RecipientPhotoUrl, opt => opt.MapFrom(src => 
               src.Recipient.Photos.FirstOrDefault(x=> x.IsMain).Url));



                       
        }
    }
}

// mục đích để đổ dữ liệu chẳng hạn khi bạn muốn đk 1 ng dùng mới cho ứng dụng của mình ,
// bạn có thể sử dụng registerdto để luuw trữ thông tin ng dùng và sử dụng appuser để lưu trữ thông tin ng dùng trong cở sở dữ liệu của ứng dụng 

