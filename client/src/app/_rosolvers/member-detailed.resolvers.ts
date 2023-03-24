import { Injectable } from '@angular/core';
import { Member } from 'src/app/_models/member';
import { ActivatedRouteSnapshot, Resolve, RouterStateSnapshot } from "@angular/router";
import { Observable } from 'rxjs';
import { MembersService } from '../_services/members.service';


@Injectable({
  providedIn: 'root'
})

export class MemberDetailedResolver implements Resolve<Member>{


  // đc khai báo và có 1 contructor với tham số memberservice là 1 dịch vj service đc sử dụng để lấy thông tin thành viên từ api

  constructor( private memberService: MembersService){

  }
  //pth resolve đc triển khai với tham số route , đại deieenjc ho tuyến đường hiện tại
  //pth này trả về về observable phát ra 1 đối tượng member

  resolve(route: ActivatedRouteSnapshot):  Observable<Member> {
    // bên trong pth này gọi hàm getmember để lấy thông tin thành viên

    return this.memberService.getMember(route.paramMap.get('username')|| '{}');
}
}
/* trong phương thức Resolve dùng để cung cấp dữ liệu cho 1 tuyến đường
cụ thể trc khi tuyến đường đó đc kích hoạt
khi ng dùng điều hướng đến 1 tuyến đường

triển khai 1 reosler cho trang chi tiết thành viên trong ứng dụng angularr
tóm lại là classmemberdeatailresolve đc sủ dụng để cung cấp dữ liệu cho trang chi tiết thành viên nó sử dụng memberservice để lấy thông tin thành viên từ api
và trả về 1 observable phát ra 1 đối tượng member


*/
