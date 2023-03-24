using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Extensions;
using API.Helpers;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    // cta thêm authorize
    [Authorize] // thêm thuộc tinh ủy quyên f
    public class MessagesController : BaseApiController
    {
        private readonly IUserRepository _userRepository;
        private readonly IMessageRepository _messageRepository;
        private readonly IMapper _mapper;
        public MessagesController(IUserRepository userRepository, IMessageRepository messageRepository, IMapper mapper)
        {
            _mapper = mapper;
            _messageRepository = messageRepository;
            _userRepository = userRepository;
            // khởi tạo các trường tham số 
        }
        //hàm để khởi tạo các trường của lớp từ các đối tượng truyền vào
        // việc khởi tạo để cung cấp các đối tượng cần thiết cho các phương thức của lớp để thực hiện các tác vụ xử lý dữ liệu





        [HttpPost]
        // tạo tin nhắn mới để gửi dữ liệu từ người dùng đến server
        public async Task<ActionResult<MessageDto>> CreatMessage(CreateMessageDto createMessageDto)  //lưu trữ thông tin về tin nhắn mới mà ng dùng muốn gửi
        {
            // lấy tên người dùng 
            var username = User.GetUsername();
        
            // kiêm tra tên ng dùng có giống tên trong tin nhă
            if (username == createMessageDto.RecipientUsername.ToLower())
                return BadRequest("You cannot send messages to yourself");
            //ktra xem người dùng có gửi tin nhắn cho chính mình hay không
            //bằng cách tên ng dùng hiện tại với tên ng dùng ng nhận


            var sender = await _userRepository.GetUserByUsernameAsync(username);
            var recipient = await _userRepository.GetUserByUsernameAsync(createMessageDto.RecipientUsername);
            //đoạn này dùng để lấy thông tin người dùng và ng nhận tin nhắn dựa vào tên ng dùng của họ


            if (recipient == null) return NotFound();
            //nếu k tìm thấy thì tra về notfound


            // SAU ĐÓ sẽ tạo 1 tin nhắn mới 
            // chứa thông tin về ng dùng và ng nhận
            var message = new Message
            {
                Sender = sender,
                Recipient = recipient,
                SenderUsername = sender.UserName,
                RecipientUsername = recipient.UserName,
                Content = createMessageDto.Content
            };
            // sau đó chuyển tới kho lưu trữ tin nhắn .sau đó cta thêm tin nhắn và chuyển tin nhắn những gì cta muốn

            _messageRepository.AddMessage(message);
            if(await _messageRepository.SaveAllAsync()) return Ok(_mapper.Map<MessageDto>(message));  // lưu trữ tin nhăn smowis vào cơ sở dữ liệu
            return BadRequest("Failed to send message");
        }


        [HttpGet]
        //getmessagesForuser đc sử dụng để lấy các tin nhắn cho ng dùng hiện tại và trả về kết quả 1 danh sách các đối tượng messagedto
        public async Task<ActionResult<IEnumerable<MessageDto>>> GetMessagesForUser([FromQuery] MessageParams messageParams)
        {
            messageParams.Username = User.GetUsername();  
            // lấy tên ng dùng từ đối tượng user( là đối tượng đại diện cho ng dùng hiện tại và gái giá trị cho thuộc tính usename)
            var messages = await _messageRepository.GetMessagesForUser(messageParams);
            // đc sử dụng để tương tác vớ cơ sở dữ liệu

            Response.AddPaginationHeader(messages.CurrentPage, messages.PageSize,
                    messages.TotalCount, messages.TotalPages);
            
            //thêm thông tin về phân trang vào header của pttp response 
            return messages;
            // sau đó trả về kết quả danh sách các tin nhắn dưới dạng actionresult để trả về cho http reponse

        
        }
        

       
        // hãy nhớ rằng cta có quyền truy cập vào tên ng dùng hiện tại trong bộ điều khiển
        // đc định như như 1 pth httpget với uri là thread/uername để trả về 1 danh sách các tin nhắn giữa ng dùng hiện tịa và 1 ng dùng khác xác định bởi username
        // trong pth async và await đc sử dụng để tạo 1 pth bất đồng bộ , có thể xử lý các tác vụ (task) đag chờ đợi trên các luồng khác nhau  
        // pth này nhận đối số username và sử dụng nó để lấy danh sách các tin nhắn giữa ng dùng hiện tại và ng dùng khác từ cở sở dữ liệu
        // sau đó pth sẽ trả về 1 đối tượng actionresult chứa 1 danh sách các tin nhắn dưới dạng 
         [HttpGet("thread/{username}")]
        public async Task<ActionResult<IEnumerable<MessageDto>>> GetMessageThread(string username)
        {
            var currentUsername = User.GetUsername();
            return Ok(await _messageRepository.GetMessageThread(currentUsername, username));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteMessage(int id )
        {
            var username = User.GetUsername();
            // lấy tên đăng nhập của ng dùng hiện tại qua đối tượng user

            var message = await _messageRepository.GetMessage(id);
            // lấy tin nhắn cần xóa từ cơ sở dữ liệu bằng cách sử dụng đối tượng
            if(message.Sender.UserName != username && message.Recipient.UserName != username ) 
            return Unauthorized();
            // kiểm tra xem gn dùng hiện tại có phải là ng gửi hoặc ng nhận tin nhắn hay không , nếu k trả về lỗi unauthozized


            if(message.Sender.UserName== username) 
            message.SenderDeleted = true;
            

            if(message.Recipient.UserName== username) 
            message.RecipientDeleted = true;

            if(message.SenderDeleted && message.RecipientDeleted)
             _messageRepository.DeleteMessage(message);
             //nếu tin nhắn đã bị xóa cả ng dùng và ng nhận thì tin nhắn sẽ đc xóa khỏi co sở dữ liệu

            if(await _messageRepository.SaveAllAsync()) return Ok();

            return BadRequest("Problem deleting message");

        }
        // httpdeleted định nghĩa để xác định pth này đc kích hoạt bởi 1 yêu cầu http delete đến đường dẫn chứa 1 tham số id 
        //id truyền vào 1 số duy nhất đại diện cho tin nhắn cần xóa , trả về đối tượng actionResult 



    } 
}

//đoạn mã trên định nghĩa 1 lớp messageController trong 1 ứng dụng ,lớp này chứa 1 pth 
/*
 lớp này chứa 1 số phương thức để quản ý giữa ng dùng trong ứng dụng 


 */