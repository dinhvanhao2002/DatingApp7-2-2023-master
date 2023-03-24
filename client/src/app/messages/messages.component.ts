import { environment } from 'src/environments/enviroment';
import { Component, OnInit } from '@angular/core';
import { getPaginationHeaders } from '../_services/paginationHelper';
import { Message } from '../_models/message';
import { Pagination } from '../_models/pagination';
import { MessageService } from '../_services/message.service';

@Component({
  selector: 'app-messages',
  templateUrl: './messages.component.html',
  styleUrls: ['./messages.component.css']
})
export class MessagesComponent implements OnInit {
  messages : Message[] = [];
  // khai báo thuộc tính message
  pagination : Pagination;

  container = 'Inbox';
  pageNumber = 1;
  pageSize= 5;
  loading = false;
  // biến này để đánh dấu quá trình chưa tải của ứng dụng



  constructor(private messageService : MessageService){

  }
  ngOnInit(): void {
    this.loadMessages();
  }

  loadMessages(){
    // khi loading đc đặt bằng true để hiện thị cho ng dùng quá trình tải đag diễn ra ,
    // khi dữ liệu đc tải từ api biến loading đc đặt lại là false để đánh dấu quá trình hoàn tất

    this.loading = true; // nếu bắt đầu tải bằng sai

    // nhận đc phản hồi và trả về đc kết quả đánh số trang
    this.messageService.getMessages(this.pageNumber, this.pageSize, this.container).subscribe(response =>{
      this.messages = response.result;
      this.pagination = response.pagination;
      this.loading = false;

    })

  }
  //dùng để tải các danh sách tin nhắn từ server và hiện thi chúng lên giao diện ng dùng
  //sử dụng pth subcribe() để đăng ký lắng nghe phản hồi server sau khi thực hiện yêu cầu lấy dữ liệu
  // khi server trả về phản hồi l danh sách các tin nhắn sẽ đc lưu vào result

  // phân trang số

  // khởi tạo thêm hàm xóa thành viên
  deleteMessage(id : number){
    //nhận tham số là id kiểu number
    //và thực hiện xóa 1 tin nhắn dựa trên id đó bằng cách gọi pt deletemessage

    this.messageService.deleteMessage(id).subscribe(()=>{
      this.messages.splice(this.messages.findIndex(m => m.id === id), 1)
      // 1 ở đây là số lượng tin nhắn muốn xoa là 1

    })

  }
  //sau khi đã tạo đc pth công việc cần làm sẽ là tới html của message button delete để tạo



  pageChanged(event : any)
  {
    this.pageNumber = event.page;
    this.loadMessages();

  }

}

//kiến thức mới pth slice( ) và splice đều là pth đc cung cấp bởi javascript để lm việc với mảng

//splice trích xuất 1 hoặc nhiều phần tử 1 mảng , tạo 1 mảng mới k ảnh hưởng đến mảng ban đầu

//splice xóa hoặc thêm ptu hoặc nhiều pt vào vảng , ảnh hưởng đến mảng ban đầu
