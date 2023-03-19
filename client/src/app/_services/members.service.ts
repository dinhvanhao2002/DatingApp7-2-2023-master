import { PaginatedResult } from './../_models/pagination';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, Observable, of } from 'rxjs';
import { environment } from 'src/environments/enviroment';
import { Member } from '../_models/member';
import { UserParams } from '../_models/userParams';

// const httpOptions  = {
//   // là 1 đối tượng đc sủ dụng để thiết lập cacs tùy chọn http request
//   // trong đối tượng này header dùng làm tiêu để cho request
//   // cho thêm ! khẳng định rằng đối tượng trả về k phải là null

//    headers : new HttpHeaders({
//       Authorization: 'Bearer ' +JSON.parse(localStorage.getItem('user')||"{}")?.token
//       //lưu vào localStorage
//    })
//   }


@Injectable({
  providedIn: 'root'
})
export class MembersService {
  baseUrl = environment.apiUrl;
  members: Member[] = [];
  //dùng để khai báo 1 mảng các đối tượng member và gán nó bằng mảng rỗng
  //khi cần lưu trữ quá nhiều đối tượng có cùng kiểu dữ liệu thì
  //bạn có thể dùng mảng để quản lý


  constructor(private http: HttpClient ) {
  }


  getMembers (userParams: UserParams){   // itemsPerPage đại diện cho số lượng ptu// phương thức này sẽ trả về 1 observables của 1 mảng

  let params = this.getPaginationHeaders(userParams.pageNumber, userParams.pageSize)

   params = params.append('minAge', userParams.minAge.toString());
   params = params.append('maxAge', userParams.maxAge.toString());
   params = params.append('gender', userParams.gender.toString());


    return this.getPaginatedResult<Member[]>(this.baseUrl + 'users',params)
    // khi mà ta bôi đen extract to method in class
  }
  private getPaginatedResult<T>(url: string ,params: any ) {
    const paginatedResult : PaginatedResult<T> = new PaginatedResult<T>();
    return this.http.get<T>(url, { observe: 'response', params }).pipe(
      map(response => {
        if(response.body!=null  && response.body !== undefined)
        {
          paginatedResult.result = response.body;
        }

        if (response.headers.get('Pagination') !== null) {
          paginatedResult.pagination = JSON.parse(response.headers.get('Pagination') || '{}');
          console.log(paginatedResult.result);
        }
        return paginatedResult;
      }),
      //map(paginatedResult => paginatedResult)
    );
  }



  private getPaginationHeaders(pageNumber: number, pageSize: number)
  {
    let params = new HttpParams();
    params = params.append('pageNumber', pageNumber.toString());
    params = params.append('pageSize', pageSize.toString());

    return params;


  }



  // thực hiện yêu cầu httpget đến 1 địa chỉ nhất định
  // nó trả về 1 obervable của một mảng các Member
  // đây là phương thức để lấy danh sách thành viên


  getMember(username: string){
    //trc khi gửi yêu cầu http thì pth sẽ tìm kiếm trong mảng members xem có thành viên nào có cùng tên đăng nhập hay k
    const member = this.members.find(x => x.username === username);
    // nếu đã tìm thấy thì trả về đối tượng của thành viên bằng toán tử of
    if( member !== undefined ) return of(member);

    // cuối cùng nếu k tìm thấy thì pth sẽ gửi yêu cầu http get tới máy chủ để lấy thông tin của thành viên
    return this.http.get<Member>(this.baseUrl + 'users/' + username)
  }

  // nó trả về 1 obervable của một đối tượng member cụ thể , tương ứng với username truyền vào(lấy từ API)

  // sau khi đã chạy đc postman đã hiện update thì vào trong memberservice
  // để tạo pt update


  updateMember(member: Member){  // nhân tham số đầu vào
    return this.http.put(this.baseUrl + 'users', member).pipe(
      map(()=>{
        const index = this.members.indexOf(member);
        this.members[index] = member;
      })
    )
  }

  setMainPhoto(photoId:number) {
    return this.http.put(this.baseUrl + 'users/set-main-photo/' + photoId, {})
  }
  //sau đó vào cpn photoedit để gọi hàm vào

  deletePhoto(photoId : number){
    return this.http.delete(this.baseUrl + 'users/delete-photo/' + photoId)

  }
  // sau đó vào photoedit để tạo hàm delete

}





















  // // nhận đối tương member là thông tin ng dùng cần cập nhật và gửi thông tin đó đi dưới dạng request
  // // sau khi yêu cầu http put đc gửi đi và máy chủ đã cập nhật thông tin thành viên . pth này sử dụng toàn tử map()
  // // để cập nhập thông tin thành viên , pth này sẽ tìm kiếm thanh viên bằng pth indexof
  // // sau đó cập nhật thông tin của thanh viên tại vị trí index trong mảng
  // setMainPhoto(photoId:number) {
  //   return this.http.put(this.baseUrl + 'users/set-main-photo/' + photoId, {})

  // }


  // getMembers (page?: number, itemsPerPage?:number ) {   // itemsPerPage đại diện cho số lượng ptu              // phương thức này sẽ trả về 1 observables của 1 mảng
  //   //console.log("goi ham trong member service")
  //   // if(this.members.length > 0 ) return of(this.members);
  //   // còn lại trg hp mảng rỗng thì nó sẽ gửi yêu cầu http get tới máy chủ để lấy danh sách
  //   //url và user sau đó sử dụng toán tử pipe để thực hiện 1 loạt các danh sách
  //   // tóm lại pt này kiểm tra nếu ds thành viên đã đc tải trc đó và trả về danh sách đó nếu có



  //   let params = new HttpParams();
  //   if (page !== null && page !== undefined && itemsPerPage !== null && itemsPerPage !== undefined) {
  //     params = params.append('pageNumber', page.toString());
  //     params = params.append('itemsPerPage', itemsPerPage.toString());
  //   }
  //   // if(page !== null && itemsPerPage !==null )
  //   // {
  //   //   params = params.append('pageNumber', page!.toString());
  //   //   params = params.append('itemsPerPage', itemsPerPage!.toString());
  //   // }
  //   return this.http.get<Member[]>(this.baseUrl + 'users', {observe: 'response', params}).pipe(
  //     map(response => {
  //       this.paginatedResult.result = response.body ?? [];
  //       if(response.headers.get('Pagination') !==null )
  //       {
  //         this.paginatedResult.pagination = JSON.parse(response.headers.get('Pagination')!);
  //       }
  //       return this.paginatedResult;
  //     })
  //   )
  // }




