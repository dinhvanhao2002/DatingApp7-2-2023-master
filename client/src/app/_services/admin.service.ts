import { User } from 'src/app/_models/user';
import { HttpClient } from '@angular/common/http';
import { environment } from './../../environments/enviroment';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class AdminService {

  baseUrl = environment.apiUrl;


  constructor(private http:HttpClient) { }

  getUsersWithRoles(){
    return this.http.get<Partial<User[]>>(this.baseUrl+ 'admin/users-with-roles');

  }

  updateUserRoles(username: string , roles: string[])
  {
    return this.http.post(this.baseUrl + 'admin/edit-roles/' + username + '?roles=' + roles, {});

  }
  // đây là pth để gửi yêu cầu http post đến 1 api đag chạy trên server , mục đích cập nhập danh sách
  //pth post gọi đối số thứ 2 là đối tượng trống {} để xác định rằng k có thân yêu cầu gửi đến http
  //pth post trả về 1 đối tượng observable, đại diện cho kết quả trả về server


}


