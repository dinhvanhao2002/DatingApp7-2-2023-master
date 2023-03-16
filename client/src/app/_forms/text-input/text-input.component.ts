import { Component, Input, OnInit, Self } from '@angular/core';
import { ControlValueAccessor, NgControl, FormControl } from '@angular/forms';


@Component({
  selector: 'app-text-input',
  templateUrl: './text-input.component.html',
  styleUrls: ['./text-input.component.css']
})
export class TextInputComponent  implements ControlValueAccessor  {  // cho phép tạo điều khiển biểu mẫu
  @Input() label: string;  // khai báo 1 string , thuộc tính có thể đặt từ bên ngoài thanh phần
  @Input() type= 'text';


  constructor(@Self() public ngControl: NgControl) {  // định nghĩa hàm xấy dựng textInputComponent ngControl
    this.ngControl.valueAccessor = this;
    console.log(this.ngControl);
    
  }

  get control():FormControl{
    return this.ngControl.control as FormControl;
  }

  // giao diện này nó 4 phương thức
  writeValue(obj: any): void {

  }
  registerOnChange(fn: any): void {
  }
  registerOnTouched(fn: any): void {
  }
  // setDisabledState?(isDisabled: boolean): void {
  // }

}
