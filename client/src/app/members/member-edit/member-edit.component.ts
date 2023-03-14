import { Member } from 'src/app/_models/member';
import { Component, HostListener, OnInit, ViewChild } from '@angular/core';
import { User } from 'src/app/_models/user';
import { AccountService } from 'src/app/_services/account.service';
import { MembersService } from 'src/app/_services/members.service';
import { take } from 'rxjs';
import { ToastrService } from 'ngx-toastr';
import { NgFor } from '@angular/common';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-member-edit',
  templateUrl: './member-edit.component.html',
  styleUrls: ['./member-edit.component.css']
})
export class MemberEditComponent implements OnInit {
  @ViewChild('editForm') editForm!: NgForm;
  // đc dùng để truy cập đối tượng con trong 1 component
  //  @ViewChild('editForm') để truy cập đối tượng editform
  //! cho phép gán giá trị null hoặc underfined cho biếntrong trường hợp k tìm thấy đối tượng
 @HostListener('window:beforeunload', ['$event']) unloadNotification($event : any){
  if(this.editForm.dirty){
    $event.returnValue = true;
  }
 }
 //HostListener đc sử dụng để đăng ký 1 hàm xử lý sự kiện cho sự kiện window
 // sự kiện này kích hoạt khi ng dùng sắp rời khỏi trang hoặc tắt tab trình duyệt



 //được sử dụng để lắng nghe sự kiện

  member!: Member;
  //biến member lưu trữ thông tin thành viên trình sử
  user !: User;
  // lưu trữ thông tin của người dùng đang đăng nhập
  // dịch vụ thành viên và điều muốn làm là điền đối tượng


  constructor(private accountServices: AccountService, private memberService: MembersService, private toastr: ToastrService) {
    this.accountServices.currentUser$.pipe(take(1)).subscribe(user=>this.user= user)

    // lấy thông tin người dùng đang đăng nhập và lữu trữ nó bằng biến user


  }
  ngOnInit(): void {
    this.loadMember();

  }

  loadMember(){
    this.memberService.getMember(this.user.username).subscribe(member=>{
      this.member= member;
      // đối tượng trả về là đối tượng member của lớp Member

    })
  }
  // phương thức loadMember()  đc sử dụng để load thông tin một thành viên từ api
  // cụ thể có thể gọi đến pt getMember() truyền vào username của user hiện tai
// sau khi thay đổi thành viên thì update

  updateMember(){
    console.log(this.member);
    this.memberService.updateMember(this.member).subscribe(member =>{

      // hiện thông báo khi update thành công
      this.toastr.success('Profile updated successfully');
      this.editForm.reset(this.member);
      // sau khi thông báo thành công thì reset lại
      // đây sẽ là thanh viên được cập nhập sau khi gửi biểu mẫu của mình

    });

    // cập nhập thông tin thành viên
  }


}


// hiện thị form chỉnh sửa thông tin cá nhân của ng dùng
