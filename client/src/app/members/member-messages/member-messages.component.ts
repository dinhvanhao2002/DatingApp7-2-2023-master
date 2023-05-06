import { NgForm } from '@angular/forms';
import { NgFor } from '@angular/common';
import { Message } from './../../_models/message';
import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { MessageService } from 'src/app/_services/message.service';

@Component({
  selector: 'app-member-messages',
  templateUrl: './member-messages.component.html',
  styleUrls: ['./member-messages.component.css']
})
export class MemberMessagesComponent implements OnInit {

// khai báo 1 biến đầu vào , đầu vào là username đc khai báo 1 chuỗi string
// tại sao lại dùng cái này là vì . nếu bạn có 1 component cha và muốn sử dụng component member-messages
// với đầu vào username để hiện thị thông tin ng dùng bạn có thể truyền như sau
//<app-user username="John Doe"></app-user>

@ViewChild('messageForm') messageForm: NgForm;
// cho phép truy cập đến 1 thành phần con hoặc 1 phần tử html trong template
//messageForm là tên biến đc khai báo để đại diện cho pt html
  @Input() messages : Message[]
//khai báo 1 thuộc tính có tên messages và kiểu dữ liệu là 1 mảng array các đối tượng message
//(private messageService: MessageService
 @Input() username: string;
  messageContent : string

  constructor(private messageService: MessageService){

  }
  ngOnInit(): void {

  }

  sendMessage(){
    this.messageService.sendMessage(this.username, this.messageContent).subscribe(message =>{
       this.messages.push(message);
       // thiết lập lại các thông số trống ở đó
       this.messageForm.reset();
    })
  }




  // loadMessages(){
  //   this.messageService.getMessageThread(this.username).subscribe(messages =>{
  //     this.messages = messages;

  //   })

  // }



}
