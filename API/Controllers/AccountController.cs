using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace API.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly DataContext _context;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;

        public AccountController(DataContext context, ITokenService tokenService, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
            _tokenService = tokenService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDTO>> Register(RegisterDTO registerDto)
        {
            if (await UserExists(registerDto.Username)) return BadRequest("Username is taken");
            // kiểm tra xem tên người dùng có tồn tại hay không, nếu tồn tại thì trả về thông báo 

            var user = _mapper.Map<AppUser>(registerDto);
            // đối tượng mapper để thực hiện ánh xa các đối tượng registerdto sang appusser

            using var hmac = new HMACSHA512();
            user.UserName = registerDto.Username.ToLower();
            user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password));
            user.PasswordSalt = hmac.Key;
            
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return new UserDTO
            {
                Username = user.UserName,
                Token = _tokenService.CreateToken(user)
            };
        }
        // http post để đăng ký người dùng mới 


        [HttpPost("login")]
        public async Task<ActionResult<UserDTO>> Login(LoginDTO loginDto)
        {
            var user = await _context.Users
            .Include(p => p.Photos)
            .SingleOrDefaultAsync(x => x.UserName == loginDto.Username);

            if (user == null) return Unauthorized("Invalid username");

            using var hmac = new HMACSHA512(user.PasswordSalt);

            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));

            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != user.PasswordHash[i]) return Unauthorized("Invalid password");
            }
            return new UserDTO
            {
                Username = user.UserName,
                Token = _tokenService.CreateToken(user),
                PhotoUrl = user.Photos.FirstOrDefault(x => x.IsMain)?.Url,
                KnownAs =  user.KnownAs,
                Gender= user.Gender

                // thuộc tính photourl của đối tượng userdto 
                // đc khởi tạo danh sách ảnh 
                //FirstOrDefault tra về ptuwr đầu tiên của danh sách , ktra xem đối tượng có rỗng hay không thì trả về mặc định là null
            };
        }

        private async Task<bool> UserExists(string username)
        {
            return await _context.Users.AnyAsync(x => x.UserName == username.ToLower());
        }

    }
}


//xử lý các yêu càu http liên quan đến xác thực ng dùng và quản lý tài khoản 
// AccountController là 1 đối tượng điều khiên trong ứng dụng , nó xử lý cac syêu cầu liên quan đến tài khoản ng dùng , chẳng hạn như đăng ký và đăng nhập 
// //   var user = new AppUser
//             {
//                 UserName = registerDto.Username.ToLower(),
//                 PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
//                 PasswordSalt = hmac.Key
//             };