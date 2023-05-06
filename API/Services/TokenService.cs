using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace API.Services
{
    public class TokenService : ITokenService
    {
        private readonly SymmetricSecurityKey _key;
        private readonly UserManager<AppUser> _userManager;
        public TokenService(IConfiguration config, UserManager<AppUser> userManager)
        {
            _userManager = userManager;
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Tokenkey"]));
        }

        // tạo tài khoản token 

        public async Task<string> CreateToken(AppUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.NameId, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName),

            };
            // sau đó cta sẽ lấy vai trog của ng dùng , để quản lý ng dùng và phân quyền
            var roles = await _userManager.GetRolesAsync(user);
            

            claims.AddRange(roles.Select(role =>new Claim(ClaimTypes.Role,role)));
            // sau khi lấy đc danh sách , tiếp theo tạo ra dánh ách các claim (tức là thông tin và quyền hạn ) bằng cách thêm các claims về vai trò vào trong danh sách claims
            


            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}


// tạo tài khoản token để quản lý và xác thực mã thông báo 
// token là chuỗi lú tự đc sử dụng để xác thực ng dùng hoặc ứng dụng. trong quá trình xác thực ng dùng/*



/*

trong quá trình xác thực , token sẽ đc sinh ra và gửi đến máy khách(client)
và máy khách sẽ sử dụng để truy cập tài nguyên đc bảo vệ 
trên máy chủ 

*/