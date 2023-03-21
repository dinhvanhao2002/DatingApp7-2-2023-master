namespace API.Entities
{
    public class Message
    {
        public int Id { get; set; }
        //đại diện khóa chính của tin nhắn

        public int SenderId { get; set; }
        // đại diện cho id của ng dùng gửi tin nhắn 

        public string SenderUsername { get; set; }
        //đại điện cho tến đăng nhập của ng gửi tin nhắn 

        public AppUser Sender { get; set; }
        //đại diện cho ng gửi tin nhắn thông qua thuộc tính này , cta có thể truy cập đến tất cả thông tin của ng dùng

        public int RecipientId { get; set; }
        // đây là thuộc tính đại diện cho id của ng nhận tin nhắn 

        public string  RecipientUsername { get; set; }
        // thuộc tính đại diện cho tến đăng nhập của ng nhận tin nhắn 

        public AppUser Recipient { get; set; }
        
    }
}

// khai báo những thuộc tính trong này 