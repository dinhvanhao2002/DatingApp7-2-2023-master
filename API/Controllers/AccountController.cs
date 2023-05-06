using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
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
        //private readonly DataContext _context;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ITokenService tokenService, IMapper mapper)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _mapper = mapper;
            _tokenService = tokenService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDTO>> Register(RegisterDTO registerDto)
        {
            if (await UserExists(registerDto.Username)) return BadRequest("Username is taken");
            // kiểm tra xem tên người dùng có tồn tại hay không, nếu tồn tại thì trả về thông báo 

            var user = _mapper.Map<AppUser>(registerDto);
            // đối tượng mapper để thực hiện ánh xa các đối tượng registerdto sang appuser

            // using var hmac = new HMACSHA512();
            user.UserName = registerDto.Username.ToLower();
            // user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password));
            // user.PasswordSalt = hmac.Key;
            //khi cta làm về chap16 thì k cần cài đặt băm mật khẩu nữa



            // _context.Users.Add(user);
            // await _context.SaveChangesAsync();
            // điều cta lm là thay đổi 2 dòng này 
            var result = await _userManager.CreateAsync(user, registerDto.Password);
            // đều tạo ra và lưu thay đổi vào cở sở dữ liệu
            // đc sử dụng để tạo tài khoản ng dùng mới . đại diện cho thoogn tin tài khoản đc ng dùng cần tạo
            //trả về đối tượn identityresult , nếu tài khoản ng dùng tạo tành công thì sẽ trả về true

            // còn ngược lại thông tin lỗi 
            if(!result.Succeeded) return BadRequest(result.Errors);

            var roleResult = await _userManager.AddToRoleAsync(user, "Member");
            // thực hiện thêm ng dùng hiện tại vào vai trò member trong hệ thống phân quyền 
            // user là đối tượng ng dùng cần đc thêm vào vai trò member
            //member là tên vai trò cần đc thêm vào 
            // nếu thành công thì IdentityResult.Succeeded sẽ có giá trị true

            if(!roleResult.Succeeded) return BadRequest(result.Errors);
            

            return new UserDTO
            {
                Username = user.UserName,
                Token =await _tokenService.CreateToken(user),
                KnownAs = user.KnownAs,
                Gender = user.Gender
            };
        }

        // http post để đăng ký người dùng mới 


        [HttpPost("login")]
        public async Task<ActionResult<UserDTO>> Login(LoginDTO loginDto)
        {
            var user = await _userManager.Users
            .Include(p => p.Photos)
            .SingleOrDefaultAsync(x => x.UserName == loginDto.Username.ToLower());
            //pth single ... đc sử dụng để truy csdl bất đồng bộ và lấy ra 1 đối tượng user duy nhất từ csdl 

            if (user == null) return Unauthorized("Invalid username");

            // using var hmac = new HMACSHA512(user.PasswordSalt);

            // var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));

            // for (int i = 0; i < computedHash.Length; i++)
            // {
            //     if (computedHash[i] != user.PasswordHash[i]) return Unauthorized("Invalid password");
            // }

            var result = await _signInManager
                    .CheckPasswordSignInAsync(user, loginDto.Password, false);
                //pth CheckPasswordSignInAsync trả về 1 đối tương siginressult đại diện cho kết quả của hoạt động ktra đăng nhập
                //nếu tài khoản khớp thì tra rveef tru còn ng claij false

            if(!result.Succeeded) return Unauthorized();
            //sau khi đăng nhập k thành công nó trả về cho ng dùng trạng thái k đc ủy quyền

            return new UserDTO
            {
                Username = user.UserName,
                Token =await _tokenService.CreateToken(user),
                PhotoUrl = user.Photos.FirstOrDefault(x => x.IsMain)?.Url,
                KnownAs = user.KnownAs,
                Gender = user.Gender

                // thuộc tính photourl của đối tượng userdto 
                // đc khởi tạo danh sách ảnh 
                //FirstOrDefault tra về ptuwr đầu tiên của danh sách , ktra xem đối tượng có rỗng hay không thì trả về mặc định là null
            };
        }

        private async Task<bool> UserExists(string username)
        {
            return await _userManager.Users.AnyAsync(x => x.UserName == username.ToLower());
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