import { User } from '../_models/user';
import { AccountService } from '../_services/account.service';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';


@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})

export class NavComponent implements OnInit {
  // oninit là những interface cung cấp bởi angular cỏe , nó bảo gồm pth ngoninit đc gọi khi component đc khởi tạo và hiện thị lần đầu tiên

  model: any = {};

  // currentUser$: Observable<User> = new Observable<User>();
  //currentUser$!: Observable<User> //đc khai báo nhưng lại k có giá trị khởi tạo
  // 2 cách đều được


  constructor(public accountService: AccountService, private router:Router, private toastr : ToastrService)
  {
    // sau khi thêm roouter vào trong contructer

  }


  ngOnInit():void{
    //this.currentUser$ = this.accountService.currentUser$;
  }
  //ngOnit() là 1 vòng đời lifecycle hook trong angular

  login(){
    
    console.log(this.model);
    this.accountService.login(this.model).subscribe(response =>{
      //khi đăng nhập thành công thì muốn nó vẫn ở phần tiếp theo của đăng ký
      // nên cần 1 đường dẫn
      this.router.navigateByUrl('/members');
    });
  }

  // phương thức login của 1 đối tượng accountSevice , đối số truyền vào ở đây là model
  // cmp nó sẽ đăng ký nhận thông tin từ sever thông qua phương thưc subcribe
  // biến loggedIn trong cmp đc gán là giá trị tru khi ng dùng đã đăng nhập thành công

  logout(){
    this.accountService.logout();
    // khi mà logout thì sẽ về trang home
    this.router.navigateByUrl('/');
  }

  // phương thức này đc sử dụng để đăng ký currentUser có thể quan sát đc của accountService
}

// k cần pt getcurrentuser nữa vì chúng tôi đang lấy nó trực tiếp
