import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, RouterStateSnapshot, UrlTree } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { map, Observable } from 'rxjs';
import { AccountService } from '../_services/account.service';

@Injectable({
  providedIn: 'root'
})
export class AdminGuard implements CanActivate {

  constructor(private accountService: AccountService, private toastr: ToastrService)
  {}

  canActivate(): Observable<boolean> {  // biểu thị quyền truy cập ng dùng
    return this.accountService.currentUser$.pipe(
      map(user => {
        if(user.roles.includes('Admin')|| user.roles.includes('Moderator') ){
          return true;
        }
        this.toastr.error('You cannot enter this area')
        return false;
      })
    )
  }

}

// map là 1 toán tử observable giúp chuyển đổi từng phần tử của observables thành giá tị mới

