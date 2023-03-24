namespace API.DTOs
{
    public class CreateMessageDto
    {
       public string RecipientUsername { get; set; } 
       //đại diện cho người nhận tin nhắn 

       public string Content { get; set; }
       // đại diện cho nội dung tin nhắn 


    }
}

// dùng để đại diện cho dữ liệu đầu vào khi tạo 1 tin nhắn mới trong ứng dụng API
// để cụ thể hơn class này chứa các thuộc tính để đại diện cho các thoogn tin cần thiết để tạo 1 tin nhắn , như nội dung tin nhắn , ng gửi , ng nhận 
