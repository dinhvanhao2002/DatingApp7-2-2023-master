using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text.Json;
using System.Threading.Tasks;
using API.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class Seed
    {

        public static async Task SeedUser(UserManager<AppUser> userManager,
        RoleManager<AppRole> roleManager )
        {
            if( await userManager.Users.AnyAsync()) return;
            var userData = await System.IO.File.ReadAllBytesAsync("Data/UserSeedData.json");
            var users = JsonSerializer.Deserialize<List<AppUser>>(userData);
            if(users == null) return;
            
            var roles = new List<AppRole>
            {
                new AppRole{Name= "Member"},
                new AppRole{Name = "Admin"},
                new AppRole{Name= "Moderator"},
            };
            // sau đó sẽ lưu vai trò vào
            foreach (var role in roles)
            {
                await roleManager.CreateAsync(role);
            }


            foreach( var user in users )
            {
                //using var hmac = new HMACSHA512();

                user.UserName = user.UserName.ToLower();
                await userManager.CreateAsync(user, "Pa$$w0rd");
                await userManager.AddToRoleAsync(user, "Member");

                //điều này yêu cầu mật khẩu phức tạp
                // và trong identiservice thì phần mở rộng tôi tắt là phẩn mở ộng k phải chữ và số bắt buộc



                // user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("Pa$$w0rd"));
                // user.PasswordSalt = hmac.Key;

                // vì chúng ta đã sử dụng chức năng nhận dạng rồi nên k cần nó nữa
                // context.Users.Add(user);

            }
            //await context.SaveChangesAsync();

            var admin = new AppUser
            {
                UserName = "admin",

            };
            await userManager.CreateAsync(admin, "Pa$$w0rd");
            await userManager.AddToRolesAsync(admin, new[]{"Admin", "Moderator"});




        }

    }
}

//được sử dụng để tạo dữ liệu ngẫu nhiên và đưa dữ liệu vào database cho mục đích phát triển và thử nghiệm ứng dụng 
// seeduser đc sử dụng để lọc dữ liệu từ json chuyển đổi thành danh sách đối tượng appuser 
// chúng ta sẽ có 1 vai trò của quản trị viên , 1 vài trò của ng điều hành và vai cho thnah viên
// bài này đang hướng đến các quyền đc sử dụng trong ứng dụng để xác điịnh những gn dùng có quyền truy cập vào những tính năng cụ thể 
//