using System;
using System.Threading.Tasks;
using API.Extensions;
using Microsoft.AspNetCore.SignalR;

namespace API.SignalR
{
    public class PresenceHub: Hub
    {
        public override async Task OnConnectedAsync()
        {
            await Clients.Others.SendAsync("UserIsOnline",Context.User.GetUsername());
            // pth này đc gọi và trả về thông điểm UserIsOnline tới các client khác thông qua pth SendAsync() 
            // thông điệp này chứa tên ng dùng (username) của client mới kết nối đc lấy thông qua thuộc tính Context.User.GetUsername();
            //thuộc tínhContext.User.GetUsername() lấy tên ng dùng username của client ddag kết nối tới server
            //khi cần xác thực và ủy quyền thì gọi hàm getusername() trong claim


        }
        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await Clients.Others.SendAsync("UserIsOffline", Context.User.GetUsername());
            await base.OnDisconnectedAsync(exception);


        }
        // sau khi đã định nghĩa các pth thì vào Startup để cúng hình services
        
    }
}


// lớp Presence này đc sử dụng để tạo kết nối giữa các client và server thông qua SignalR
//SignalR là 1 thư viện cho phép giao tiếp hai chiều giữa client và server bằng cách sử dụng giao thức web socket và long-polling 
