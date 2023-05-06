import { ToastrService } from 'ngx-toastr';
import { environment } from 'src/environments/enviroment';
import { Injectable } from '@angular/core';
import { HubConnection } from '@microsoft/signalr';
import { User } from '../_models/user';
import { HubConnectionBuilder } from '@microsoft/signalr/dist/esm/HubConnectionBuilder';

@Injectable({
  providedIn: 'root'
})
export class PresenceService {
  // lấy đường dẫn
  hubUrl= environment.hubUrl;
  private hubConnection : HubConnection;


  constructor(private toastr: ToastrService) { }
// chúng tôi cần gửi mã thông báo ng dùng, nên cta sẽ truyền đối tượng user vào
// cta sẽ thiết lập kết nối và nhận thông tin giữa máy khách và máy chủ
// thông qua đó bảo đảm tính năng hiện thị sự có mặt của ng dùng trên ứng dụng web

  creatHubConnection(user: User){
    //tham số truyền vào chưa thông tin ng dùng , bao gồm 1 token đc xử dụng để xác thực ng dùng
    this.hubConnection = new HubConnectionBuilder()
    .withUrl(this.hubUrl + 'presence',{
      accessTokenFactory: () =>user.token
      // cung cáp access token để xác thực ng dùng khi kết nối
    })
    .withAutomaticReconnect()
    //tự động kết nối lại khi mất kết nối vơi server
    .build();
    // xấy dựng kết nối sau đó bắt đầu

    this.hubConnection
        .start()
        //start đc gọi là kết nối bắt đầu
        .catch(error=> console.log(error)
         )
         // catch sử lý lỗi ngoại lệ
    this.hubConnection.on('UserIsOnline',username =>{
      this.toastr.info(username + 'has connected')
    })
    // cuối cùng lắng nghe sự kiện user is online từ server và hiện thị thông báo toastr
    this.hubConnection.on("UserIsOffline",username =>{
      this.toastr.warning(username + 'has disconnected')
    })

    // Note : userisonline và userisoffline phải đúng với cấu hình ban đầu mình đã định dạng ở presenhub
    // tạo pth dừng kết nối
  }
  
  stopHubConnection(){
    this.hubConnection.stop().catch(error => console.log(error))
  }

}
