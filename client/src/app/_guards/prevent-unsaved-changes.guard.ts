import { MemberEditComponent } from './../members/member-edit/member-edit.component';
import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanDeactivate, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class PreventUnsavedChangesGuard implements CanDeactivate<unknown> {
  canDeactivate(
    component: MemberEditComponent)  : boolean {
      // nó cho phép kiêm tra 1 điều kiện nhất định
      if(component.editForm.dirty){
        return confirm('Are you sure you want to continue ? Any unsaved changes will be lost')
      }
    return true;
    //có thể hủy kích hoạt hoàn thành
    // true nó giữ nguyên biểu mẫu
    // sau đó vào rooting-modle để thêm router

    }

}
