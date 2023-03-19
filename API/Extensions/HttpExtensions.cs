using System.Text.Json;
using API.Helpers;
using Microsoft.AspNetCore.Http;

namespace API.Extensions
{
    public static class HttpExtensions
    {
        public static void AddPaginationHeader(this HttpResponse response , int currentPage, int itemsPerPage, int totalItems, 
        int totalPages )
        {

            var paginationHeader = new PaginationHeader(currentPage, itemsPerPage, totalItems, totalPages);  // tạo 1 đối tượng với các tham số truyền vào 

            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            response.Headers.Add("Pagination", JsonSerializer.Serialize(paginationHeader, options));
            response.Headers.Add("Access-Control-Expose-Headers","Pagination");

        }
        
    }
}

// pth này định nghĩa 1 pth mở rộng cho lớp httpResponse
// lớp httpResponse dùng để đại diện phản hồi từ server đến client 
//khi 1 client yêu cầu 1 tài nguyên từ server , server sẽ trả lại phản hồi http dưới dạng 1 đối tượng httpreponse 
