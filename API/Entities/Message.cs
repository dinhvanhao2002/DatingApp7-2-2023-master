using System;

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

        public string Content { get; set; }

        public DateTime? DataRead { get; set; }
        // Thuộc tính dataread đại diện cho ngày giờ mà tin nhắn đã đc đọc bởi ng nhận , cho phép lưu trữ thông tin về ngày giờ 
        //thuộc tính này hiện thị cho người gửi biết tin nhắn của họ đã đc đọc hay chưa 

        public DateTime MessageSent { get; set; } = DateTime.Now;
        // định dạng ngày giờ tin nhắn đc gửi đi
        // đặt thời gian cho dấu thời gian của máy chủ hiện tại

        public bool SenderDeleted { get; set; }
        //đại diện trạng thái của ng gửi tin nhắn đối với tin nhăn snay f
        // ban đầu là 1 kiểu dữ liệu bool , có giá trị là true , nếu ng dùng gửi tin nhắn đã xóa tin nhắn 
        //và false nếu tin nhắn vẫn chưa bị xóa

        public bool RecipientDeleted { get; set; }
        //đại diện cho trạng thái của tin nhắn . nếu ng nhận tin nhắn quyết điịnh xóa tin nhắn thì thuộc tính sẽ đặt thành true 
        
    }
}

// khai báo những thuộc tính trong này 
// tóm lại là trong lớp message định nghĩa 1 tin nhắn trong người dùng , với thông tin về ng gửi , ng nhận và nội dung tin nhắn .Các thuộc tinh của lớp cho phép truy cập đến các thông tin này trong cở sở dữ liệu 
//và cho phép các thao tác đọc và ghi dữ liệu