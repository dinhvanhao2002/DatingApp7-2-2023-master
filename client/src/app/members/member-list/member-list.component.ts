import { AccountService } from 'src/app/_services/account.service';
import { Pagination, PaginatedResult } from './../../_models/pagination';
import { Observable, take } from 'rxjs';
import { Component } from '@angular/core';
import { Member } from 'src/app/_models/member';
import { MembersService } from 'src/app/_services/members.service';
import { UserParams } from 'src/app/_models/userParams';
import { User } from 'src/app/_models/user';

@Component({
  selector: 'app-member-list',
  templateUrl: './member-list.component.html',
  styleUrls: ['./member-list.component.css']
})
export class MemberListComponent {
  members : Member[] = [];

  pagination: Pagination;
  // pageNumber= 1;
  // pageSize = 5;

  userParams: UserParams;
  user : User;
  genderList = [{value: 'male', display: 'Males'},{value:'female',display: 'Females'}];





  //members$! : Observable<Member[]> ;
  // chứa dữ liệu danh sách thành viên
  // members$ là biến chứa dữ liệu danh sách các thanh viên đc sử dụng hiển thị lên màn hình

  constructor(private memberService: MembersService)
  {
    this.userParams = this.memberService.getUserParams();


  }


  ngOnInit(): void {
    // this.loadMembers();
    this.loadMembers();

   // this.members$ = this.memberService.getMembers();
    // sau khi pth getmembers ddc gọi , dữ liệu danh sách các thành viên sẽ đc trả về
    //dưới dạng 1 observable và đc lưu vào biến members$ để hiện thị lên màn hình

  }
  loadMembers(){
      this.memberService.setUserParams(this.userParams);
      this.memberService.getMembers(this.userParams).subscribe(response =>{
        this.members = response.result;
        this.pagination = response.pagination;
        console.log(this.pagination);
        //console.log('lỗi ở đây');
      })
  }

  // hàm reserFilters
  resetFilters() {
    this.userParams = this.memberService.resetUserParams();  // đặt lại giá trị ban đầu
    this.loadMembers();

  }
  //hàm này dùng ddeerresset lại các tham số tìm kiếm ng dùng và load lại danh sách thanh viên
  //người dùng thực hiện tìm kiếm danh sách thành viên và các tham số tìm kiếm đc lưu trong đối tượn userparams
  // sau đó loadmembers sẽ gọi lại danh sách mới


  pageChanged(event : any) {
    this.userParams.pageNumber = event.page;
    this.memberService.setUserParams(this.userParams);
    console.log(this.userParams.pageNumber);
    this.loadMembers();
  }
  // hàm sự kiện đc gọi khi trang hiện tại đc thay đổi trong thành phần phân trang
  // tham số event chưa thông tin trang mới đc chọn và đc truyền vào hàm qua thông qua đối số event
  //





  }

  // loadMembers(){
  //   this.memberService.getMembers().subscribe(members =>{
  //     this.members = members;
  //   },error=>{
  //     console.log("loi cho nay")
  //   })

  // }


