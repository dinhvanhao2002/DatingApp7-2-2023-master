import { PresenceService } from './presence.service';
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
  constructor(private http:HttpClient, private presence: PresenceService) { }

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
          //khi login thì cta cần thiết lập present để tạo kết nối luôn
          this.presence.creatHubConnection(user);

         }
      })
    )
  }

  register(model: any) {
    return this.http.post<User>(this.baseUrl + 'account/register', model).pipe(
      map((user: User) => {
         if (user) {
          this.setCurrentUser(user);
          this.presence.creatHubConnection(user);



         // console.log(this.setCurrentUser(user));
         //  localStorage.setItem('user', JSON.stringify(user));
          //  this.currentUserSource.next(user);
          //cta có thể thay đổi ng dùng

         }
      })
    )
  }
  // pth pipe đc sử lý trả về kết quả từ server

  setCurrentUser(user: User) {
      // để sử dụng lưu trữ cục bộ
      // tới nơi ng dùng hiện tại cấp cho ng dùng
      user.roles = [];  // gán cho mảng rỗng của đối tượng user
      const roles = this.getDecodedToken(user.token).role;
      console.log(roles);

      // để giải mã token và trả về đối tượng json chứa thông tin giải mã
      //thuộc tinh roles nó trả về 1 danh sách
      //kiểm tra vai trò của chúng là 1 mảng hay là chuỗi
      Array.isArray(roles) ? user.roles =roles : user.roles.push(roles);
      // nếu nó là mảng đối tượng user sẽ cập nhật với biến roles
      //nếu k phải là mảng thì nó đẩy mang user.roles

      localStorage.setItem('user', JSON.stringify(user));
      this.currentUserSource.next(user);
    }

    logout() {
      localStorage.removeItem('user');
      this.currentUserSource.next(null!);
      this.presence.stopHubConnection()
    }
    getDecodedToken(token: string)
    {
      // lấy thông tin mã thông báo
      return JSON.parse(atob(token.split(".")[1]));

    }
    // việc lấy thông tin từ jwt để xác thực và ủy quyền cho các tác vụ tỏng ứng dụng web
    // nó sẽ tạo ra chuỗi token lúc đó ta có thể lên jwt,io để biết đc phân quyền của tên username này

    // mã thông báo jwt đc chia thành 3 phần header ,payload (chứa dữ liệu) và signatura (chữ kí số)
    // các phần này nó đc tách thành dấu chấm


}


