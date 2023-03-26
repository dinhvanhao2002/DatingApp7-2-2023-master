using System.Linq;
using System.Threading.Tasks;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class AdminController : BaseApiController // kế thừa từ basecontroller 
    {
        private readonly UserManager<AppUser> _userManager;
        public AdminController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        [Authorize(Policy = "RequireAdminRole")]
        // thuộc tính xác thực và quyền truy cập của pth hoặc action
        [HttpGet("users-with-roles")]

        // trả về danh sách ng dùng vơi từng vai trò
        public async Task<ActionResult> GetUsersWithRoles()
        {
           // return Ok("Only admins can see this");
           //cái này chỉ là kiêm tra bước đầu xem đã nhận đc chưa 
           // quản lý người dùng như sua 
            var users = await _userManager.Users // khởi tạo biến user để chứa danh sách ng dùng
                .Include(r => r.UserRoles) // đc chỉ định cho rằng muốn lấy thêm thông tin// lấy các bản ghi liên quan đến thuộc tinh UserRoles
                .ThenInclude(r=>r.Role) // để lấy thêm các bản ghi liên quan thuộc tính role
                .OrderBy(u=> u.UserName) /// sắp xếp danh sách ng dùng theo tên
                .Select(u=> new
                {
                    u.Id,  // id của ng dùng, tên đăng nhập, danh sách các vai trò mà ng dung fcos 
                    Username = u.UserName,
                    Roles = u.UserRoles.Select(r=> r.Role.Name).ToList()
                    
                })
                // chọn các trường dữ liệu trả về và định dạng lại các giá trị 
                .ToListAsync();  // chuyển kết quả truy vấn sang danh sách ng dùng
            // truy vấn danh sách ng dường trong hệ thông 

            return Ok(users);

        }
        // chỉnh sửa vai trò của người dùng 
        [HttpPost("edit-roles/{username}")]
        // thuộc tính route để chỉ định đường dẫn ult cho thường thước editroles

        public async Task<ActionResult> EditRoles(string username,[FromQuery] string roles ) // nhận đầu vào là username và roles
        {  
            // async để chỉ ra pth bất đồng bộ và sẽ trả về Task<Result>
            // khi thực hiện các tạc 
            var selectedRoles = roles.Split(',').ToArray();  // tách chuỗi , sau đso
            // dòng này để tách roles thành 1 mảng dựa trên dấu phẩy 
            //bằng pth split sau đó lưu trữ chúng vào 1 mảng bằng pth toarray
            var user = await _userManager.FindByNameAsync(username); // tìm kiếm ng dùng có tên đăng nhập tương ứng trong cơ sở dữ liệu
            if(user == null) return NotFound();
            
            var userRoles = await _userManager.GetRolesAsync(user); // cung cấp vai trò của ng dùng, lấy danh sách các vai tròn
            var result = await _userManager.AddToRolesAsync(user, selectedRoles.Except(userRoles));  // trả về 1 mảng chuỗi con k có trong danh sách các vai trò hiện tại của ng dùng
            if(!result.Succeeded) return BadRequest("Failed to add to roles");


            result = await _userManager.RemoveFromRolesAsync(user, userRoles.Except(selectedRoles));

            if(!result.Succeeded) return BadRequest("Failed to remove");

            return Ok(await _userManager.GetRolesAsync(user));

        }

        [Authorize(Policy = "RequireAdminRole")]
        [HttpGet("photos-to-moderate")]
        // trong đoạn mã này xử lý các yêu cầu request get tới đường dẫn photos-to-modorate

        public ActionResult GetPhotosForModeration()
        {
            return Ok("Admin or moderators can see this");

        }
    }
}