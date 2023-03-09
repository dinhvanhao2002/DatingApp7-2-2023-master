using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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

        public UsersController(IUserRepository userRepository, IMapper mapper)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }

        [HttpGet]
        // [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<MemberDto>>> GetUsers()
        {
            var users = await _userRepository.GetMembersAsync();
            return Ok(users);
        }
        // lấy tất cả các thành viên trong cơ sở dữ liệu thông qua pt GetMembersAsync
        // sau đó danh sách này sẽ đc chuyển đổi thành danh sách memberdto


        //api/users/3
        //[Authorize]
        [HttpGet("{username}")]
        public async Task<ActionResult<MemberDto>> GetUser(string username)
        {
            return  await _userRepository.GetMemberAsync(username);

        }
        
        [HttpPut]
        public async Task<ActionResult> UpdateUser(MemberUpdateDto memberUpdateDto)
        {
            var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            // dùng để lấy tên đăng nhập của ng dùng hiện tại 
            // như này sẽ cung cấp cho cta tên ng dùng từ mã thông báo mà API sử dụng để xác thực
            // lấy giá trị thuộc tính 

            var user = await _userRepository.GetUserByUsernameAsync(username);
            // lấy thông tin ng dùng hiện tại từ database 
            
            _mapper.Map(memberUpdateDto, user);
            // sau đó cta sẽ cập nhật lại , ánh xạ lại thông tin của dto vào trong user


            _userRepository.Update(user);

            if( await _userRepository.SaveAllAsync()) return NoContent();
            // sử dụng để lưu trữ lại thay đổi trong database
            // nếu k thành công serve trả về mã trạng thái http 400 bad request

            return BadRequest("Failed to update user");
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







*/