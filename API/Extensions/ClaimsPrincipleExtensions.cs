using System.Security.Claims;

namespace API.Extensions
{
    public static class ClaimsPrincipleExtensions
    {
        public static string GetUserName(this ClaimsPrincipal user)
        {
            return  user.FindFirst(ClaimTypes.NameIdentifier)?.Value; 

        }
        // ở đây đã tạo đc hàm gọi username 
    }
}

