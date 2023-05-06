namespace API.Helpers
{
    public class PaginationHeader
    {
        public PaginationHeader(int currentPage, int itemsPerPage, int totalItems, int totalPages)
        {
            CurrentPage = currentPage;
            ItemsPerPage = itemsPerPage;
            TotalItems = totalItems;
            TotalPages = totalPages;
        }

        public int CurrentPage { get;set;}

        public int ItemsPerPage { get; set; }

        public int TotalItems { get; set; }

        public int TotalPages { get; set; }
        // đây tất cả những thông tin cta muốn gửi từ api đến phía client
        

    }
}


// có thể đc sử dụng để tạo header phân trang trong phản hồi http trả về từ api 
// api sẽ sử lý yêu cầu và trả về 1 phản hồi http chứa các dữ lieueddc yêu cầu 
//chúng ta sẽ gửi từ api đến phía client , định nghĩa các tham số cần thiết cho việt phân trang 

// sau đó vào Iuerrepository để chỉnh sửa truy xuất danh sách thành viên truyền dữ liệu giữa các đối tượng