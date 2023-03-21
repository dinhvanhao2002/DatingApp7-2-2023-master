import { Pagination } from './../_models/pagination';
import { Component, OnInit } from '@angular/core';
import { Member } from '../_models/member';
import { MembersService } from '../_services/members.service';

@Component({
  selector: 'app-lists',
  templateUrl: './lists.component.html',
  styleUrls: ['./lists.component.css']
})
export class ListsComponent implements OnInit  {
  members : Partial<Member[]>;
  //Partial cho phép tạo 1 phiên bản mới và loại bỏ đi các thuộc tính bắt buộc , giúp nó khởi tạo 1 đối tượng
  //dễ dàng hơn , nó chứa 1 đối tượn Member có thể thiếu 1 số thuộc tính bắt buộc
  predicate = 'liked';
  // thêm phân trang vào đây
  pageNumber = 1;
  pageSize = 5;
  pagination : Pagination;

  constructor(private memberService: MembersService) {
  }
  // trong hàm khởi tạo chúng ta đưa vào các dịch vụ

  ngOnInit(): void {
    this.loadLikes();


  }

  // LOADLikes, trong pth getMembers ở memberserv
  loadLikes() {
    this.memberService.getLikes(this.predicate, this.pageNumber, this.pageSize).subscribe(response=>{
      this.members = response.result;
      this.pagination = response.pagination;
    })
  }

  // thêm hàm phân trang trong html
  pageChanged(event: any){
    this.pageNumber = event.page;
    this.loadLikes();
  }

}
