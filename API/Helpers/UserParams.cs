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
// là lớp lưu trữ các tham số tìm kiếm và ng dùng thông qua thông số phân trang

// lớp này nó kế từa lớp phân trang , để thêm các thuộc tính đặc biệt cho trang tìm kiếm ng dùng 
