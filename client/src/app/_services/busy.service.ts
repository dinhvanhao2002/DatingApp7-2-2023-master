import { Injectable } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';

@Injectable({
  providedIn: 'root'
})
export class BusyService {
  busyRequestCount = 0;
  // để biết rằng k có yêu cầu nào được xử lý , khi yêu cầu gửi đến thì
  //giá trị biến tăng lên bằng pt busy , giảm bằng pt idle()
  //

   constructor(private spinnerServices: NgxSpinnerService) { }
   busy(){
      this.busyRequestCount ++;
      // tăng giá trị lên 1 và hiển thị spinner để biểu thị đang bận rộn
      // tăng lên 1 thì hiện thị showw

      this.spinnerServices.show(undefined, {
        type: 'line-scale-party',
        bdColor: 'rgba(255, 255, 255,0)',
        color:'#333333'
      });
      // hiện thị bằng cái đặt nhất định 

   }
   idle(){
    this.busyRequestCount --;
    if(this.busyRequestCount <=0){
      this.busyRequestCount =0;
      this.spinnerServices.hide();

    }
   }
}


// sử lý phần spinner
