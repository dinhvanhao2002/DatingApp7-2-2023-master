using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Helpers;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class MessageRepository : IMessageRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public MessageRepository(DataContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;

        }

        public void AddMessage(Message message)
        {
            _context.Messages.Add(message);
        }
        // pth này nó thêm 1 tin nhắn mới vào dataset


        public void DeleteMessage(Message message)
        {
            _context.Messages.Remove(message);
        }

        public async Task<Message> GetMessage(int id)  // chấp nhận tham số là id của tin nhắn mà cta muốn lấy 
        {
            // return await _context.Messages.FindAsync(id);
             return await _context.Messages
                    .Include(u => u.Sender)  // đưa thêm thông tin liên quan đến gn gửi và ng nhận của tin nhắn ,điều này là để khi cta gọi các pth này , nó sẽ tự động lấy thông tin liên quan đến ng gửi và ng nhận
                    .Include(u=> u.Recipient)
                    .SingleOrDefaultAsync(x => x.Id == id);
                    // pth singordefault để tìm 1 tin nhắn với Id cho id phù hợp đc truyền vào 
                    //và pth này nó sex trả về đối tượng messages duy nhất hoặc null nếu k tìm thấy tin nhắn phù hợp
                    

        }
        //pth này truy xuất 1 tin nhắn từ dbset , dựa trên thuộc tính id


        public async Task<PagedList<MessageDto>> GetMessagesForUser(MessageParams messageParams)
        {
            var query = _context.Messages
                .OrderByDescending(m => m.MessageSent) // sắp xếp chúng theo thời gian gửi tin nhắn mới nhất
                .AsQueryable();
            // tạo toán tử switch để xác định hộp thư nào đag được truy cập 
            //u.RecipientDeleted == false) kiểm tra tin nhắn có bị ng nhận xóa hay không , thông qua trường recipinetdeleted 
            // nếu recipientdeleted = true tức là tin nhắn đã bị ng nhận xóa , thì tin nhắn sẽ k đc lấy ra 
            // nếu bằng false thì tin nhắn chưa bị ng nhận xóa 
            query = messageParams.Container switch  
            {
                "Inbox" => query.Where(u => u.Recipient.UserName == messageParams.Username 
                && u.RecipientDeleted == false),  // inbox hộp thư đến 
                //inbox hộp thư đến tức là đứa nhận đc tin nhắn 


                "Outbox" => query.Where(u => u.Sender.UserName == messageParams.Username && 
                u.SenderDeleted== false), //outbox hộp thư đã gửi
                //hộp thư đã gửi tức là đã gửi tin nhắn cho đứa khác r

                _ => query.Where(u => u.Recipient.UserName == messageParams.Username && u.RecipientDeleted== false && u.DataRead == null) 
                //tin nhắn chưa đọc
            };

            var messages = query.ProjectTo<MessageDto>(_mapper.ConfigurationProvider);
            return await PagedList<MessageDto>.CreateAsync(messages, messageParams.PageNumber, messageParams.PageSize);
        }
        
        //pth này sử dụng để lấy danh sách các tin nhắn từ DbSet

        public  async Task<IEnumerable<MessageDto>> GetMessageThread(string currentUsername, 
            string recipientUsername)
        {
            var messages = await _context.Messages
            // đây là câu truy vấn dữ liệu từ cơ sở dữ liệu thông qua đối tượng ỎM đc lưu trong biến _context

                .Include(u=> u.Sender).ThenInclude(p=>p.Photos)
                .Include(u=> u.Recipient).ThenInclude(p=> p.Photos)
                // sử dụng pth Include để tải thêm thông tin về ng dùng và ng nhận 
                // để tải thông tin về ảnh đại diện của ng dùng 
                


                // sủ dụng pth where để lọc danh sách tin nhắn theo 2 điều kiện 
                // tin nhắn có ng gửi là currnetuser và ng nhận là recipinet
                .Where(m=> m.Recipient.UserName == currentUsername && m.RecipientDeleted == false
                    && m.Sender.UserName == recipientUsername
                    || m.Recipient.UserName == recipientUsername
                    && m.Sender.UserName == currentUsername && m.SenderDeleted ==false
                )
                .OrderBy(m => m.MessageSent)  // sắp xếp danh sách tin nahwns theo thời gian gửi
                .ToListAsync();  // để thực hiện truy vấn và chuyển danh sách tin nhắn sang kiểu list

 
            var unreadMessages = messages.Where(m => m.DataRead == null && 
               m.Recipient.UserName == currentUsername).ToList();  

            // tạo biến unread messages bằng cách sử dụng pth where để lấy danh sách các tin nhắn chưa được đọc\
            //sau đó pt Tolisst đc sử dụng để chuyển danh sách thành 1 danh sách mới 


            // kiểm tra xem có chứa bất kì tin nhắn nào k, bằng cách sử dụng pth any 
            // nếu có các tin nhắn trong danh sách sẽ đc gán là đã đọc
            if(unreadMessages.Any())
            {
            // dùng foreach để duyệt qua từng đối tượng tin nhắn trong danh sachs unread..

                foreach(var message in unreadMessages )
                {
                    message.DataRead =DateTime.Now;
                }
                await _context.SaveChangesAsync();
                // và lưu vào cơ sở dữ liệu bằng pth savechangesAsync
            }
            return _mapper.Map<IEnumerable<MessageDto>>(messages);
            // cuối cùng pth sẽ trả về 1 danh sách các đối tượng messageDto , 
            //đc tạo ra bằng pth map của 1 thư viện ánh xạ đối tượng để ánh xạ các đối tượng tin nhắn messages thành đối tượng mesagedto

            
        }
        // đây là 1 phương thức của 1 dịch vụ cụ thể lấy danh sách các tin nhắn trong 1 cuộc trò chuyện giữa 2 ng dùng cụ thể 
        // nó trả về 1 danh sách đối tượng MessageDto , mỗi đối tượng đại diện cho 1 tin nhắn trong cuộc trò chuyện giữa 2 ng dùng 
        // sử dụng async/ await để chạy bất đồng bộ 
        // nhwunxg gì chúng ta làm ở đây sẽ chuyển đến bộ điều khiển để thực hiện điều này và sẽ thêm 1 pth vào đây




        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}