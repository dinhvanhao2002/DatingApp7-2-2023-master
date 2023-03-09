using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Interfaces
{
    public interface IUserRepository
    {
        void Update(AppUser user);

        Task<bool> SaveAllAsync();
        Task<IEnumerable<AppUser>> GetUsersAsync();
        Task<AppUser> GetUserByIdAsync(int id);
        Task<AppUser> GetUserByUsernameAsync(string username);
        Task<ActionResult<AppUser>> GetUserByUsernameAsync();
        

        Task<IEnumerable<MemberDto>> GetMembersAsync();
        
        Task<MemberDto> GetMemberAsync(string username);

        //  Task<PagedList<MemberDto>> GetMembersAsync(UserParams userParams);
        // Task<MemberDto> GetMemberAsync(string username);

    }
}


// đây là bước đầu khởi tạo repository , sử dụng để tương tác với các dữ liệu đc lưu trữ trong cơ sở dữ liệu 
// cung cấp 1 lớp trung gian giữa các lớp dịch vụ và cơ sở dữ liệu 
// vong đời bao gồm khởi tạo , truy vấn , xử lý , dữ liệu , lưu trữ và giải phóng bộ nhớ


// webser server là phần nềm đc cài trên máy chủ(serverr) để đáp ứng c
//các yêu cầu của ng dùng thông qua internet hoặc mạng nội bộ
// chức năng nhận các yêu cầu http từ trình duyệt 
