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
  // members: Member[] = [];
  members$! : Observable<Member[]> | undefined ;
  // chứa dữ liệu danh sách thành viên
  // members$ là biến chứa dữ liệu danh sách các thanh viên đc sử dụng hiển thị lên màn hình
  

  constructor(private memberService: MembersService){}

  ngOnInit(): void {
    // this.loadMembers();
    this.members$ = this.memberService.getMembers();
  }

  }

  // loadMembers(){
  //   this.memberService.getMembers().subscribe(members =>{
  //     this.members = members;
  //   },error=>{
  //     console.log("loi cho nay")
  //   })

  // }


