using API.Data;
using API.Helpers;
using API.Interfaces;
using API.Services;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Extensions
{
    public static class ApplicationServiceExtenstions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            services.Configure<CloudinarySettings>(config.GetSection("CloudinarySettings"));
            // cấu hình các thông tin cần thiết cho dịch vụ luuw trữ đám mây
            services.AddScoped<ITokenService, TokenService>();

            services.AddScoped<IPhotoService, PhotoService>();

            services.AddScoped<ILikesRepository, LikesRepository>();

            services.AddScoped<LogUserActivity>();  // đăng ký dịch vụ

        

            services.AddScoped<IUserRepository, UserRepository>();

            services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);
            // cấu hình đăng ký để thực hiện ánh xạ các đối tượng trong ứng dụng
            services.AddDbContext<DataContext>(options =>
            {
                // để tương tác với cơ sở dữ liệu
                options.UseSqlite(config.GetConnectionString("DefaultConnection"));
            });
            return services;
        }
    }
}

//được sử dụng để đăng ký dịch vụ cần thiết cho ứng dụng 

// nó dùng để cấu hình các dịch vụ đăng ký 

// quan trọng nè : sau khi mà đăng ký các dịch vụ cta có thê sử dụng các class hoặc controllers khác của ứng dụng thông qua hệ thông dependency ịnection
