import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ReplaySubject } from 'rxjs';
import {map} from 'rxjs/operators';
import { environment } from 'src/environments/enviroment';
import {User} from '../_models/user';


@Injectable({
  providedIn: 'root'
})
export class AccountService {
  baseUrl = environment.apiUrl; // đường dẫn đến api
  private currentUserSource = new ReplaySubject<User>(1);  // 1 ở đây nói số phiên bản của ng dùng hiện tại có
  currentUser$ = this.currentUserSource.asObservable();
  // phương thức để cấu hình người dung hiện tại, currUsersource là đc khởi tạo bởi replay
  // cho phép lắng nghe các phiên đăng nhập trc đó , `currentUser$` nó nhận lại các phiên đăng nhập

  // contructor nhận tham chiếu đến 1 đối tượn http client
  constructor(private http:HttpClient) { }

  // contructor nhận tham chiếu đến 1 đối tượn http client
  login(model: any) {
    console.log('lỗi ở đây');

    return this.http.post<User>(this.baseUrl + 'account/login', model).pipe(
      map((reponse: User) => {
         const user = reponse;
         if (user) {
          //  localStorage.setItem('user', JSON.stringify(user));
          //  this.currentUserSource.next(user);
          this.setCurrentUser(user);
         }
      })
    )
  }

  register(model: any) {
    return this.http.post<User>(this.baseUrl + 'account/register', model).pipe(
      map((user: User) => {
         if (user) {
          //  localStorage.setItem('user', JSON.stringify(user));
          //  this.currentUserSource.next(user);
          //cta có thể thay đổi ng dùng
          this.setCurrentUser(user);


         }
      })
    )
  }
  setCurrentUser(user: User) {
      // để sử dụng lưu trữ cục bộ
      localStorage.setItem('user', JSON.stringify(user));

      this.currentUserSource.next(user);
    }
    logout() {
      localStorage.removeItem('user');
      this.currentUserSource.next(null!);
    }
}
