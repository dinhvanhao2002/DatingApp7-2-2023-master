using System;

namespace API.DTOs
{
    public class MessageDto
    {
         public int Id { get; set; }
        //đại diện khóa chính của tin nhắn

        public int SenderId { get; set; }
        // đại diện cho id của ng dùng gửi tin nhắn 

        public string SenderUsername { get; set; }
        //đại điện cho tến đăng nhập của ng gửi tin nhắn 

        public string SenderPhotoUrl { get; set; }

        public int RecipientId { get; set; }
        // đây là thuộc tính đại diện cho id của ng nhận tin nhắn 

        public string  RecipientUsername { get; set; }
        // thuộc tính đại diện cho tên đăng nhập của ng nhận tin nhắn 

        public string RecipientPhotoUrl { get; set; }

        

        public string Content { get; set; }

        public DateTime? DataRead { get; set; }
        // Thuộc tính dataread đại diện cho ngày giờ mà tin nhắn đã đc đọc bởi ng nhận , 
        //cho phép lưu trữ thông tin về ngày giờ 
        //thuộc tính này hiện thị cho ng ng gửi biết tin nhắn của họ đã đc đọc hay chưa 

        public DateTime MessageSent { get; set; } 
        // định dạng ngày giờ tin nhắn đc gửi đi
        // đặt thời gian cho dấu thời gian của máy chủ hiện tại


        
    }
}