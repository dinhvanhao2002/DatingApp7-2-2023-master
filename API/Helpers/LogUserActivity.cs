using System;
using System.Threading.Tasks;
using API.Extensions;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace API.Helpers
{
    public class LogUserActivity : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var resultContext = await next();
            // kiểm tra xem ng dùng có xác thực k
            if(!resultContext.HttpContext.User.Identity.IsAuthenticated) return;


            var userId = resultContext.HttpContext.User.GetUserId();
            // sau đó truy cập vào kho dữ liệu
            var repo = resultContext.HttpContext.RequestServices.GetService<IUserRepository>();
            var user = await repo.GetUserByIdAsync(userId);
            user.LastActive = DateTime.Now;
            
            await repo.SaveAllAsync();
        }
    }
}

// IAsyncActionFilter là một interface trong ASP.NET Core, được sử dụng để xác định một bộ lọc cho một hoạt động bất đồng bộ trên một controller.
//khi 1 yêu cầu http đc gửi tới server cái IAsync.. thực hiện 1 số xử lý trc khi hoạt động bất đồng bộ đc gọi
// vd nó kiểm tra xác thực xác định ngôn ngữ hoặc ghi nhật kí trc khi hoạt động
//chuyển hướng trang và thêm thông tin vào phản hồi

//onActionExcutionAsync đc sử dụng để thực hiện các bộ lọc trc khi hoạt động bất đồng bộ đc gọi 

// để sd LogUserActivity như  1 bộ lọc nó phải đc đăng ký trong pth CongiguseService  thông qu ApplicationsService