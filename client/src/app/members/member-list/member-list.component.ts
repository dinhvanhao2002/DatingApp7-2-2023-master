import { Observable } from 'rxjs';
import { Component } from '@angular/core';
import { Member } from 'src/app/_models/member';
import { MembersService } from 'src/app/_services/members.service';

@Component({
  selector: 'app-member-list',
  templateUrl: './member-list.component.html',
  styleUrls: ['./member-list.component.css']
})
export class MemberListComponent {
  // members !: Member[] = [];
  

  members$! : Observable<Member[]> ;
  // chứa dữ liệu danh sách thành viên
  // members$ là biến chứa dữ liệu danh sách các thanh viên đc sử dụng hiển thị lên màn hình

  constructor(private memberService: MembersService){}

  ngOnInit(): void {
    // this.loadMembers();
    this.members$ = this.memberService.getMembers();
    // sau khi pth getmembers ddc gọi , dữ liệu danh sách các thành viên sẽ đc trả về
    //dưới dạng 1 observable và đc lưu vào biến members$ để hiện thị lên màn hình

  }


  }

  // loadMembers(){
  //   this.memberService.getMembers().subscribe(members =>{
  //     this.members = members;
  //   },error=>{
  //     console.log("loi cho nay")
  //   })

  // }


