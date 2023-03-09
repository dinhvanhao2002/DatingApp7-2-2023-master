import { Member } from 'src/app/_models/member';
import { Component, OnInit } from '@angular/core';
import { User } from 'src/app/_models/user';
import { AccountService } from 'src/app/_services/account.service';
import { MembersService } from 'src/app/_services/members.service';
import { take } from 'rxjs';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-member-edit',
  templateUrl: './member-edit.component.html',
  styleUrls: ['./member-edit.component.css']
})
export class MemberEditComponent implements OnInit {

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
    // hiện thông báo khi update thành công
    this.toastr.success('Profile updated successfully');

  }


}


// hiện thị form chỉnh sửa thông tin cá nhân của ng dùng
