using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Extensions
{
    public static class IdentityServiceExtensions
    {
        public static IServiceCollection AddIdentityService(this IServiceCollection services, IConfiguration config)
        {
            // đây là pth mở rộng iservicecollection dc sử dụng để đăng ký các dịch vụ cần thiết cho identity 
            //ban đầu sẽ addidentitycore đc sử dụng để đky các dịch vụ cơ bản cần thiết để hoạt động identity
            //bao gồm đăng nhập và xác thực ng dùng ,trong đoạn mã nay thì cta đăng ký appuser làm lớp đại diện cho ng dùng

            services.AddIdentityCore<AppUser>(opt => 
            {
                opt.Password.RequireNonAlphanumeric= false;
                // cấu hình yêu cầu mật khẩu của ng dùng
                //nếu bạn muốn sử dụng mật khẩu yếu bạn cần phải tắt tất cả các tùy chọn
                
            })
                .AddRoles<AppRole>()  //đăng ký vãi trò đc sử dụng trong ứng dụng
                .AddRoleManager<RoleManager<AppRole>>() //quản lý các vai trò trong ứng dụng
                .AddSignInManager<SignInManager<AppUser>>() // signinmanage để xác thực ng dùng và quản lý các phiên đăng nhập
                .AddRoleValidator<RoleValidator<AppRole>>()// để xác thwucj các vai trò được xử dụng trong ứng dụng
                .AddEntityFrameworkStores<DataContext>();  // đăng ký các dịch vụ cần thiết để lưu dữ dữ liệu
            // cấu hình Identity 
        
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
              .AddJwtBearer(options =>
              {
                  options.TokenValidationParameters = new TokenValidationParameters
                  {
                      ValidateIssuerSigningKey = true,
                      IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"])),
                      ValidateIssuer = false,
                      ValidateAudience = false,
                  };

                  // cấu hình sự kiện cho jwtbeareroptions đc sd để cấu hình xác thực với ng dùng bằng json
                //

                  options.Events = new JwtBearerEvents // khởi tạo đối tượng để xác thực ng dùng bằng jwwt
                  {
                    OnMessageReceived = context =>  //đc sử lý các yêu cầu signalr
                    {
                        //bên trong đây cta có thể lưu mã 
                        var accessToken = context.Request.Query["access_token"];
                        //lấy mã thông báo access token

                        //sau đó cta lấy đường dẫn 
                        var path = context.HttpContext.Request.Path;
                        if(!string.IsNullOrEmpty(accessToken ) && path.StartsWithSegments("/hubs"))
                        {
                            //kiểm tra xem access có giá trị và yêu cầu với hubs hay k nếu đúng cta lưu vào
                            context.Token = accessToken;

                        }
                        return Task.CompletedTask;
                        // xác nhận xử lý của cta đã hoàn thành

                    }
                  };
              });
            
            services.AddAuthorization(opt =>
            {
                opt.AddPolicy("RequireAdminRole", policy=> policy.RequireRole("Admin"));// cho phép ng dùng có vai trò admin mới có quyền truy cập
                opt.AddPolicy("ModeratePhotoRole", policy=> policy.RequireRole("Admin", "Moderator")); // cho phép ng dùng có vai trò admin hoăc moderator ms có quyền truy cập

            });
            // đây là pth để sử dụng cấu hình authoriztion 
            //services.AddAuthorization để đăng ký 


            return services;
        }
    }
}
