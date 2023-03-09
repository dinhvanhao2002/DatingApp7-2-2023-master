import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/enviroment';
import { Member } from '../_models/member';

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
  constructor(private http: HttpClient ) {

  }
  getMembers (): Observable<Member[]> {
    //console.log("goi ham trong member service")
    return this.http.get<Member[]>(this.baseUrl + 'users');
  }
  // nó trả về 1 obervable của một mảng các Member


  getMember(username: string){
    return this.http.get<Member>(this.baseUrl + 'users/' + username)
  }
  // nó trả về 1 obervable của một đối tượng member cụ thể , tương ứng với username truyền vào(lấy từ API)
  //




}


