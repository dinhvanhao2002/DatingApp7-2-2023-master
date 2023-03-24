using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Helpers;

namespace API.Interfaces
{
    public interface IMessageRepository
    {
         void AddMessage( Message message );

         void DeleteMessage( Message message );

         Task<Message> GetMessage(int id);
         // pth này dùng để lấy 1 tin nhắn cụ thể từ cơ sở dữ liẹu

         Task<PagedList<MessageDto>> GetMessagesForUser(MessageParams messageParams);
         // pth này dùng để lấy danh sách các tin nhắn, cho phép phân trang và truy suất tối đa số lượng tin nhắn
         //cta muốn cung cấp cho ng dùng cơ hội xem hộp thư đến của họ và các thư chưa đọc bên trong 
         // trả về 1 danh sách trang


         Task<IEnumerable<MessageDto>> GetMessageThread(string currentUsername, string recipientUsername);
        // pth này dùng để lấy 1 luồng tin nhắn giữa ng dùng và ng nhận
        // thay đổi bằng string currentUsername và 


         Task<bool>SaveAllAsync();
         // dùng lưu các thay đổi đc thực hiện trên cơ sở dữ liệu
         
        
    }
}

// trong đây xác định các pth 