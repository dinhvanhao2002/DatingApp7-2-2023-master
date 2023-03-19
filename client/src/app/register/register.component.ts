import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AccountService } from './../_services/account.service';
import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, ValidatorFn, Validators } from '@angular/forms';
import { FormControl } from '@angular/forms';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
    @Output() cancelRegister = new EventEmitter();
   // model: any ={}; // any nó cho bất kì giá trị nào
    registerForm: FormGroup ;
    maxDate: Date;

    // khai báo thêm
    validationErrors: string[] = [];



  constructor(private accountService: AccountService, private toastr: ToastrService,
    private fb: FormBuilder, private router: Router ) {
  }

  ngOnInit(): void {
    this.initializeForm();
    // thực hiện các thao tác khởi tạo và cấu hình cho component trc khi nó hiện thị lần đầu tiên trên giao diện
    this.maxDate= new Date();
    this.maxDate.setFullYear(this.maxDate.getFullYear() -18);
    console.log(this.maxDate);  // xem nó đã lấy đc chứa

  }

  // hàm khởi tạo biểu mẫu đăng ký với 3 trường ,,,,
  initializeForm(){
    this.registerForm =  this.fb.group({
      gender: ['male'],
      username: ['', Validators.required], // kiểm tra trường đã đc điền hay chưa
      knownAs: ['', Validators.required],
      dateOfBirth: ['', Validators.required],
      city: ['', Validators.required],
      country: ['', Validators.required],
      password: ['', [Validators.required,

      Validators.minLength(4), Validators.maxLength(8)]], // viec truyền mảng xác định truyền nhieuf validate
      confirmPassword: ['',[Validators.required, this.matchValues('password')]]
    })
  }
  // validator để đảm bảo tính hợp lệ của dữ liệu nhập vào từ người dùng ch các trg trong mẫu biểu
  // matchValues(matchTo: string): ValidatorFn{
  //   return (control: AbstractControl) =>{
  //     return control?.value === control?.parent?.controls[matchTo as any]?.value ? null : {isMatching: true};
  //   }
  // }

  matchValues(matchTo: string):ValidatorFn{
    return (control:AbstractControl) =>{
      return control?.value === control?.parent?.get(matchTo)?.value
      ? null : {isMatching: true};
    }
  }

  // lúc này sẽ k cần this.model nữa
  register() {
      console.log(this.registerForm.value);
      this.accountService.register(this.registerForm.value).subscribe(response =>{
        // console.log(response);
        // this.cancel();
        this.router.navigateByUrl('/members');
        this.toastr.success('Đăng ký thành công');

      }, error => {
        //  console.log(error);
        //  this.toastr.error(error.error);
        this.validationErrors= error;
       })
    }
    cancel() {
       this.cancelRegister.emit(false);
    }
}


























  // matchValues(matchTo: string): ValidatorFn {
  //   return (control: AbstractControl): ValidationErrors | null => {
  //     const parentControl = control.parent as FormGroup; // Sử dụng kiểu dữ liệu tường minh ở đây
  //     if (!parentControl) {
  //       return null;
  //     }
  //     return control.value === parentControl.get(matchTo)?.value ? null : { isMatching: true };
  //   };
  // }

