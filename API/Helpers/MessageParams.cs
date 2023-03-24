namespace API.Helpers
{
    public class MessageParams : PaginationParams
    {
        public string Username { get; set; }  
        //đây là ng dùng đăng nhập của chúng ta

        public string Container { get; set; }= "Unread";
        //trả lại theo mặc đinh, các tin nhắn chưa đọc của ng dùng


        
    }
}

// sau khi đã tạo đc class thì chuyển đến kho dữ liệu tin nhắn của mình để truyền params vào 
