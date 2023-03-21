namespace API.Helpers
{
    public class UserParams : PaginationParams
    {
        public string CurrentUsername { get; set; }

        public string  Gender { get; set; }

        public int MinAge { get; set; } = 18;
        public int MaxAge { get; set; } = 150;

        // thêm thuộc tính sắp 
        public string  OrderBy { get; set; } = "lastActive";


        

    
    }
}


// để dịnh nghĩa các tham số cần thiết cho việc phân trang và lọc dữ liệu trong api
// khi client gửi yêu caauf đến server , thông qua lớp userparams cta có thể đọc các tham số từ query string của request và sử dụng chúng để lọc và phân trang dữ liệu phù hợp vs yêu cầu client 
