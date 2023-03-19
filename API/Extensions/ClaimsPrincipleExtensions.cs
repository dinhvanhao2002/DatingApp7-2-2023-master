using System.Security.Claims;

namespace API.Extensions
{
    public static class ClaimsPrincipleExtensions
    {
        public static string GetUserName(this ClaimsPrincipal user)
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

