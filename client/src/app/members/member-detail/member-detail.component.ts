import { MessageService } from 'src/app/_services/message.service';
import { Message } from 'src/app/_models/message';
import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { NgxGalleryAnimation, NgxGalleryImage, NgxGalleryOptions } from '@kolkov/ngx-gallery';
import { TabDirective, TabsetComponent } from 'ngx-bootstrap/tabs';
import { Member } from 'src/app/_models/member';
import { MembersService } from 'src/app/_services/members.service';

@Component({
  selector: 'app-member-detail',
  templateUrl: './member-detail.component.html',
  styleUrls: ['./member-detail.component.css']
})
export class MemberDetailComponent implements OnInit{

  @ViewChild('memberTabs', {static: true}) memberTabs: TabsetComponent;
  // dùng để truy cập vào 1 phần tử html hoặc 1 directive trong template của component
  // stratic: true đc sủ dụng để chỉ định rằng thành phần con có thể truy cập trực tiếp quá trình khởi tạo

  member : Member;
 //  member : any;
   galleryOptions: NgxGalleryOptions[];
   galleryImages: NgxGalleryImage[];
   activeTab : TabDirective;  //
   messages : Message[] = [];
   //@Input() username : string;





   // để ! như này cho rằng biến k phải null hoặc undefined
  // ví dụ như chúng ta khơi tạo tin nhắn riêng tư và c

   constructor(private memberService: MembersService, private router: ActivatedRoute, private messageService: MessageService) {

   }

   ngOnInit(): void {
    // pth ngOnInit đc sử dụng để lắng nghe các dữ liệu đc trả về từ resoler thông qua đối tượng router
    //hàm subcribe đc gọi để lắng nghe sự kiện và cập nhập thanh viên đc hiện thi trên trang chi tiết
    this.router.data.subscribe(data=>{
      this.member = data.member;

    })

     //this.loadMember();
    // đến đây nới mà cta muốn truy cập những gì cta đag lm
    this.router.queryParams.subscribe(params =>{
      params.tab ? this.selectTab(params.tab) : this.selectTab(0);

    })


     this.galleryOptions=[
       {
         width: '500px',
         height: '500px',
         imagePercent: 100,
         thumbnailsColumns: 4,
         imageAnimation: NgxGalleryAnimation.Slide,
         preview: false,

       }
     ]
     this.galleryImages=this.getImages();


   }

   getImages(): NgxGalleryImage[]{
     const imageUrls = [];
     for(const photo of this.member.photos){
       imageUrls.push({
         small: photo?.url,
         medium: photo?.url,
         big: photo?.url
       })
     }
     return  imageUrls;
   }

   // đc sử dụng để lấy đường dẫn hình ảnh của thành viên và chuyển đổi chúng thành mảng của các đối tượng NgxGallery
   // vì this.galleryOptions đcịnh cấu hình cho thư viện ảnh NgxGallery để hiện thị các hình ảnh cho các thnahf viên trên trang chi tiết
   


   loadMember(){
     this.memberService.getMember(this.router.snapshot.paramMap.get('username')||"").subscribe(member => {
       this.member = member;
        // this.galleryImages=this.getImages();
        //cta k cần thực hiện thư viện ảnh trên trong đây

        //

     }, error =>{
       console.log(error);
     })

   }
   // đưa hàm loadtinnhawns hội thoại vào đây

   loadMessages(){



    // nó sẽ gọi pth get... của dịch vụ kia để lấy thông tin về chuỗi tin nhắn
    //this.member.username là 1 thuộc tính của đối tượng member đc xác định xong component
    // biến member có thể chứa thông tin 1 thành viên truyền vào thông quan input binding
    //thuộc tính username đc giả định là 1 chuoxi đại diện cho tên đăng nhập của thanh viên đó
    // lấy tên người dùng từ tên thành viên

    this.messageService.getMessageThread(this.member.username).subscribe(messages =>{
      this.messages = messages;

    })

  }

   //onTab
  // tạo 1 phương thức kích hoạt sự dụng tabs, nhận tham số là dữ liệu kiểu tabdirection


   onTabActivated( data: TabDirective)
   {
    // gan đối tượng tabdirective hiện tịa cho 1 biến activetab
      this.activeTab = data;
      // kiểm tra tab hiện tịa là tab messages và chưa có tin nhắn nào thì loadMessage để ltair các tin nhắn mới
      if( this.activeTab.heading === 'Messages' && this.messages.length === 0)
      {
        this.loadMessages();
      }

   }

   // tạo selecttab

   selectTab(tabId: number){
    this.memberTabs.tabs[tabId].active = true;
    console.log(this.memberTabs.tabs[tabId].active);


   }
   //pth này được dùng để chọn 1 tab trong giao diện gn dùng có nhiều tab
   //pth này có 1 tham số duy nhất tabid đó là định danh số của tab cần chọn
   //pth sau đó truy cập thuộc tinh membertabs


}
