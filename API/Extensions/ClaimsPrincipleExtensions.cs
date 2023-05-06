using System.Security.Claims;

namespace API.Extensions
{
    public static class ClaimsPrincipleExtensions
    {
        public static string GetUsername(this ClaimsPrincipal user)
        {
            return user.FindFirst(ClaimTypes.Name)?.Value; 

        }
        // ở đây đã tạo đc hàm gọi username 
        public static int GetUserId(this ClaimsPrincipal user)
        {
            // trả về số nguyên Id cho ng dùng
            return int.Parse(user.FindFirst(ClaimTypes.NameIdentifier)?.Value); 
            
           // sau đó vào logUserActivity để thay đổi 
            
        }
    }
}

/*
ClaimsPrincipal  là 1 đối tượng đc sử dụng trong hệ thống xác thực và ủy quyền , nó bao gồm nhiều danh sách claims những thông tin liên quan đến ng dùng, chẳng hạn như tên đăng nhập , 
và 1 identity đại diện cho ng duùngo ntgrosng hệ tho xacst thực 




*/

