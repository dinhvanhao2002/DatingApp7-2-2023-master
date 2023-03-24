
import { environment } from 'src/environments/enviroment';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { getPaginatedResult, getPaginationHeaders } from './paginationHelper';
import { Message } from '../_models/message';


@Injectable({
  providedIn: 'root'
})
export class MessageService {
  baseUrl = environment.apiUrl;


  constructor( private http: HttpClient) {

  }

  getMessages(pageNumber: number , pageSize: number, container: string){
    let params = getPaginationHeaders(pageNumber, pageSize);
    //tạo biến params để luuw trữ các tham số phân trang
    params = params.append('Container', container);

    return getPaginatedResult<Message[]>(this.baseUrl +'messages',params, this.http);
    // ham này sẽ tạo đối tượng PaginatedResult để lưu trữ kết quả phân trang , sau đó sử dụng pth gét trên đối tượng httpclinet
    //để gọi api
  }
  // tạo hàm cuộc hội thoại giữa ng nhận và người gửi

  getMessageThread(username : string ){
    return this.http.get<Message[]>(this.baseUrl + 'messages/thread/'+ username )
  }

  //tao phwuong thức  gửi tin nhăn s
  sendMessage( usernam : string , content :string )
  {
    return this.http.post<Message>(this.baseUrl + 'messages', {recipientUsername: usernam, content}

    )
  }

  deleteMessage(id: number)
  {
     return this.http.delete(this.baseUrl + 'messages/'+ id)
  }


}


