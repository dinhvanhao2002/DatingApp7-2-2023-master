import { RolesModalComponent } from './../modals/roles-modal/roles-modal.component';
import { AdminService } from './../_services/admin.service';
import { Component, OnInit } from '@angular/core';
import { User } from '../_models/user';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';

@Component({
  selector: 'app-user-management',
  templateUrl: './user-management.component.html',
  styleUrls: ['./user-management.component.css']
})
export class UserManagementComponent implements OnInit {
  users : Partial<User[]>;
  bsModalRef: BsModalRef;


  constructor(private adminService: AdminService, private modalService: BsModalService) {

  }


  ngOnInit(): void {
    // load danh sachs leen
    this.getUsersWithRoles();
  }

  getUsersWithRoles()
  {
    this.adminService.getUsersWithRoles().subscribe(users =>{
      this.users= users;
    })
  }
  //
  openRolesModal(user: User){  // hàm này sử dụng để mở 1 modal có chứa các thông tin về vai trò ng dùng , nó nhận vào đối tượng user
   //đối tượng config đẻ cấu hình cửa sổ modal
    const config = {
      class: 'modal-dialog-centered',
      // sử dụng để căn giữa các modal

      initialState: {
        // chứa nhiều dữ liệu ban đầu của modal
        user,
        roles: this.getRolesArray(user)

      }
    }
    this.bsModalRef = this.modalService.show(RolesModalComponent, config);

    this.bsModalRef.content.updateSelectedRoles.subscribe((values: any[])=>{
      const rolesToUpdate = {
        roles: [...values.filter(el=> el.checked === true).map(el=> el.name)]
      };
      if(rolesToUpdate)
      {
        this.adminService.updateUserRoles(user.username, rolesToUpdate.roles).subscribe(()=>
        {
          user.roles = [...rolesToUpdate.roles];
        })
      }


    })
  }


//hàm này đc sử dụng để lấy danh sách các vai tro có sẵn
  private getRolesArray(user:any){
    const roles:any = [];
    const userRoles = user.roles;
    const availableRoles: any[] = [
      {name: 'Admin', value: 'Admin'},
      {name: 'Moderator', value: 'Moderator'},
      {name: 'Member', value: 'Member'}

    ];
    availableRoles.forEach(role=> {
      let isMatch =false;
      for(const userRole of userRoles)
      {
        if(role.name === userRole)
        {
          isMatch = true;
          role.checked = true;
          roles.push(role);
          break;
        }
      }
      if(!isMatch )
      {
        role.checked = false;
        roles.push(role);
      }
    })

    return roles;

  }

}
