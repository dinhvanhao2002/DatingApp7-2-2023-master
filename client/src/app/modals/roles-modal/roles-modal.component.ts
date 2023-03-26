import { Component, EventEmitter, Input, OnInit } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { User } from 'src/app/_models/user';

@Component({
  selector: 'app-roles-modal',
  templateUrl: './roles-modal.component.html',
  styleUrls: ['./roles-modal.component.css']
})
export class RolesModalComponent implements OnInit {
  @Input() updateSelectedRoles = new EventEmitter()
  // event emitter là 1 đối tượn cho phép tạo ra các sự kiện tùy chỉnh và phát ra chúng từ 1 thành phần angular
  // đối tườn này có thể nhận dữ liệu từ thành phần cha
  
  user: User;
  roles: any[];


  // title: string;
  // list: any[] = [];  // danh sachs
  // closeBtnName : string;

  constructor(public bsModalRef: BsModalRef){

  }
  ngOnInit(): void {

  }

  updateRoles(){
    this.updateSelectedRoles.emit(this.roles);  //emit phát ra
    // sử dụng đối tượn eventemit để phát ra tùy chỉnh , với dữ liệu và danh sách các role

    this.bsModalRef.hide();
    // sau khi sự kiện đc phát ra thì sẽ đc hiện thị bằng pth hide()

  }
  // đc gọi khi ng dùng cập nhật danh sách các role và nhấn nút luu trong modal (hộp thoại hiện thị trên giao diện web)


}
