import { ControlValueAccessor, FormControl, NgControl } from '@angular/forms';
import { Component, Input, OnInit, Self } from '@angular/core';
import { BsDatepickerConfig } from 'ngx-bootstrap/datepicker';

@Component({
  selector: 'app-date-input',
  templateUrl: './date-input.component.html',
  styleUrls: ['./date-input.component.css']
})
export class DateInputComponent implements ControlValueAccessor {
  // khai báo thuộc tính
  @Input() label: string;
  @Input() maxDate: Date;
  bsConfig?: Partial<BsDatepickerConfig>;




  constructor(@Self() public  ngControl : NgControl ){
    this.ngControl.valueAccessor= this;
    this.bsConfig = {
      containerClass: 'theme-red',
      dateInputFormat: 'DD MMMM YYYY' // ĐỊNH DẠNG NGÀY THÁNG NĂM


    }
  }

  get control():FormControl{
    return this.ngControl.control as FormControl;
  }




  writeValue(obj: any): void {
  }


  registerOnChange(fn: any): void {

  }
  registerOnTouched(fn: any): void {

  }
  setDisabledState?(isDisabled: boolean): void {
   
  }


}




// là 1 interface đc sử dụng để kết nối giữa thành phần giao diện gn dùng và dữ lieuj của chúng trong form
