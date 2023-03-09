import { AccountService } from './../_services/account.service';
import { ToastrService } from 'ngx-toastr';
import { map } from 'rxjs/operators';
import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {

  constructor(private accountService: AccountService, private toastr: ToastrService) {}
  CanActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean | UrlTree | Observable<boolean | UrlTree> | Promise<boolean | UrlTree> {
    throw new Error('Method not implemented.');
  }

  canActivate(): Observable<boolean>{
    return this.accountService.currentUser$.pipe(
      map((user) => {
        if (user) return true;
        else {
          this.toastr.error('You shall not pass!')
          return false;
        }
      })
    )
  }

}


//
//AuthGuard  đc sử dụng để ktra tính hợp lệ của việc truy cập vào các trang
//nó kiểm tr XEM NG DÙNG ĐÃ ĐĂNG NHẬP hay chwua
// nếu ng dùng đăng nhập thì trả về true 
