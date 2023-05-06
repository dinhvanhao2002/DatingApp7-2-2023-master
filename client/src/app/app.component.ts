import { PresenceService } from './_services/presence.service';
import { AccountService } from './_services/account.service';
import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { User } from './_models/user';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'The Dating app';
  users : any;

  constructor(private accountService: AccountService, private presence: PresenceService) {}
  //truyền

  ngOnInit() {
     this.setCurrentUser();
  }

  setCurrentUser(){
    const user : User = JSON.parse(localStorage.getItem('user')!);
    // khai báo user và gán giá trị bằng đối tượng đc phân tích cú pháp json từ chuỗi lưu trong localstorage với key user
    if(user)
    {
      this.accountService.setCurrentUser(user);
      this.presence.creatHubConnection(user);
      // khi tạo nó , cta có quyền truy cập vào mã thông báo jwt của ng dùng

    }

  }

  }
