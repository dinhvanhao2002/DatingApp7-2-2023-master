using API.Data;
using API.DTOs;
using API.Entities;
using API.Extensions;
using API.Helpers;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Authorize]
    public class UsersController : BaseApiController
    {

        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IPhotoService _photoService;

        public UsersController(IUserRepository userRepository, IMapper mapper, IPhotoService photoService)
        {
            _photoService = photoService;
            _mapper = mapper;
            _userRepository = userRepository;
        }

        [HttpGet]
        // [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<MemberDto>>> GetUsers([FromQuery]UserParams userParams)
        {
            var user = await _userRepository.GetUserByUsernameAsync(User.GetUserName());
            // lấy thông tin ng dùng hiện tại bằng pth GetUserBy..
            userParams.CurrentUsername = user.UserName;

            if(string.IsNullOrEmpty(userParams.Gender))
            {
                userParams.Gender = user.Gender =="male" ? "female" : "male";
            }

            var users = await _userRepository.GetMembersAsync(userParams);
            Response.AddPaginationHeader(users.CurrentPage, users.PageSize,
             users.TotalCount, users.TotalPages);

            return Ok(users);
        }
        // lấy tất cả các thành viên trong cơ sở dữ liệu thông qua pt GetMembersAsync
        // sau đó danh sách này sẽ đc chuyển đổi thành danh sách memberdto


        //api/users/3
        //[Authorize]
        // cta nên đặt tên cho nó 

        [HttpGet("{username}", Name ="GetUser")]
        public async Task<ActionResult<MemberDto>> GetUser(string username)
        {
            return await _userRepository.GetMemberAsync(username);

        }

        [HttpPut]
        public async Task<ActionResult> UpdateUser(MemberUpdateDto memberUpdateDto)
        {
            // var username = User.GetUserName();
            // dùng để lấy tên đăng nhập của ng dùng hiện tại 
            // như này sẽ cung cấp cho cta tên ng dùng từ mã thông báo mà API sử dụng để xác thực
            // lấy giá trị thuộc tính 

            var user = await _userRepository.GetUserByUsernameAsync(User.GetUserName());
            // lấy thông tin ng dùng hiện tại từ database 

            _mapper.Map(memberUpdateDto, user);
            // sau đó cta sẽ cập nhật lại , ánh xạ lại thông tin của dto vào trong user


            _userRepository.Update(user);

            if (await _userRepository.SaveAllAsync()) return NoContent();
            // sử dụng để lưu trữ lại thay đổi trong database
            // nếu k thành công serve trả về mã trạng thái http 400 bad request
            return BadRequest("Failed to update user");
        }




        // [HttpPost("add-photo")]
        //bộ điều khiển ng dùng cho phép ng dùng thêm 1 ảnh mới 
        [HttpPost("add-photo")]
        // yêu cầu server thêm 
        public async Task<ActionResult<PhotoDto>> AddPhoto(IFormFile file)
        {
            // lấy tên ng dùng để xác nhận quyền sở huwx 
            var user = await _userRepository.GetUserByUsernameAsync(User.GetUserName());
            // await đc sử dụng để chờ đợi pt getuserbyusername trả về đối tượng user đc lưu trữ trong biến user

            // như vậy đã có đối tượng ng dùng ở đya 
            //đc sử dụng để lấy thông tin người dùng đang đăng nhập trong ứng dụng web
            //đối tượng userRepository đc sử dụng để tìm kiếm ng dùng theo tên đăng nhập
            // việc sử dụng user cta có thể lưu trữ thông tin về ng dùng và sử dụng 

            var result = await _photoService.AddPhotoAsync(file);
            // pth này đc sử dụng để tải ảnh lên 

            if(result.Error != null) return BadRequest(result.Error.Message);
            // nếu thuộc tính lỗi này k phải là null, điều đó xảy ra lỗi 

            var photo = new Photo
            {
                Url = result.SecureUrl.AbsoluteUri,
                PublicId = result.PublicId
            };
            // kiểm tra xem ng dùng có bất kì ảnh nào trong hồ sơ 
            // nếu ng dùng k có ảnh  thì thuộc tính heienj true 
            // có nghĩa là ảnh đã đc thêm vào và đc chọn là ảnh chính 
            
            if( user.Photos.Count ==0)
            {
                photo.IsMain = true;
            }
            user.Photos.Add(photo);
            // sau khi thêm ảnh xong thì lưu ảnh vào cơ sở dữ liệu

            if( await _userRepository.SaveAllAsync())
            {
                // pth đc sử dụng để chuyển đổi thực thể ảnh mới đc tạo thành dto 
                // đối tượng trả về là pth PhotoDto
             // return _mapper.Map<PhotoDto>(photo);
                return CreatedAtRoute("GetUser", new{username = user.UserName}   ,_mapper.Map<PhotoDto>(photo));
                // thay vì trả ra đối tượng photodto trực tiếp 
                // pt created phản hồi và bao gồm vị trí tiêu đề , tiêu đề chỉ định url mà ảnh ms tạo có thể truy cập đc
                // mục đích tạo new object để tạo 1 route tới oth getuser trong controller hiện tại và truyền giá trị username nhưu 1 tham số

            }

            return BadRequest("Problem adding photo");

        }


        [HttpPut("set-main-photo/{photoId}")]
        //PTH trả về Actionresult có thể là 1 trang web hiện thị các hình ảnh mới thành viên , hoặc thông báo 1 quá trình đặt làm hình nah thất bại
        public async Task<ActionResult> SetMainPhoto(int photoId){
            var user = await _userRepository.GetUserByUsernameAsync(User.GetUserName());

            var photo = user.Photos.FirstOrDefault(x => x.Id == photoId);
            if(photo.IsMain) return BadRequest("This is already your main photo");
            var currentMain = user.Photos.FirstOrDefault(x=> x.IsMain);
            if(currentMain!= null) currentMain.IsMain = false;
            photo.IsMain= true;

            if(await _userRepository.SaveAllAsync()) return NoContent();
            return BadRequest("Failed to set main photo");

        }

        [HttpDelete("delete-photo/{photoId}")]
        public async Task<ActionResult> DeletePhoto(int photoId){
            var user = await _userRepository.GetUserByUsernameAsync(User.GetUserName());
            var photo = user.Photos.FirstOrDefault(x => x.Id == photoId);
            if( photo == null) return NotFound();
            if(photo.IsMain) return BadRequest("You cannot delete a photo");
            if(photo.PublicId != null)
            {
                 var result = await _photoService.DeletePhotoAsync(photo.PublicId);
                 if(result.Error !=null) return BadRequest(result.Error.Message);

            }
            user.Photos.Remove(photo);

            if(await _userRepository.SaveAllAsync()) return Ok();
            return BadRequest("Failed to delete photo");


        }

    }
}

// được sử dụng để xủ lý các yêu cầu api liên quan đến user
// Imapper đc dùng để ánh xạ các đối tượng giữa các lớp khác nhau


// phân biệt httpget và httpost 
/*
httpget đc sử dụng để yêu cầu server trả về thông tin đc yêu cầu dưới dạng body reponse
body response có thể chứa nhiều thông tin như html , css , hình ảnh , âm thanh 
nó sử dụng để lấy thông tin , truy xuất dữ liệu từ server


còn đối với httppost dùng để gửi dữ liệu từ client đến sever 
thông qua body của request
nó có thể tạo mới , cập nhật hoặc xóa dữ liệu trên client
vd như mà đk nhập thông tin tài khoản hoặc mật khẩu của bạn sẽ đc gửi lên server thông qua pt httppost



httpput sử dụng cập nhập 1 tài nguyên hiện có trên máy chủ , và đc sử dụng với 1 tài nguyên 
khi 1 yêu cầu đc gửi đến máy chủ , yêu cầu này sẽ bao gồm thông tin về tài nguyên cần đc cập nhật cùng vs dữ liệu mới đc cập nhật
http put đc sử dụng rộng rãi trong việc xây dựng restful api , cho phép ng dùng cập nhật thông tin của 1 tài nguyên 

note: var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;


controller là 1 phần mô hình mvc trong lập trình web , đc sử dụng để đkh logic xử lý và phản hồi các yêu cầu request từ ng dùng
đến ứng dụng web 
controller này nó nhận yêu cầu từ client , lấy dữ liệu 
chứa các pth để xử lý yêu cầu get pos put delete từ 
client và sử dụng các model và server




*/

