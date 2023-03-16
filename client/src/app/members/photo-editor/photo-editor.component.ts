import { Photo } from './../../_models/photo';
import { AccountService } from './../../_services/account.service';
import { environment } from './../../../environments/environment.prod';
import { Member } from 'src/app/_models/member';
import { Component, Input, OnInit } from '@angular/core';
import { FileUploader } from 'ng2-file-upload';
import { User } from 'src/app/_models/user';
import { take } from 'rxjs';
import { MembersService } from 'src/app/_services/members.service';

@Component({
  selector: 'app-photo-editor',
  templateUrl: './photo-editor.component.html',
  styleUrls: ['./photo-editor.component.css']
})
export class PhotoEditorComponent implements OnInit {

  @Input() member!: Member;

  uploader!: FileUploader;  // đối tượng file upload, đại diện cho trình tải tệp lên serve
  hasBaseDropzoneOver = false;  // là biến boolean sử dụng để ktra xem con trỏ chuột có đnag nă trên vùng dropzone hay k
  baseUrl = environment.apiUrl;  // chuỗi chưa đường link api server
  user!: User;  // đối tượng server đại diện cho thông tin ng dùng đăng nhập vào ứng dụng

  // khai báo 1 số thuộc tính sau

  constructor(private accountService: AccountService, private memberService: MembersService) {
    // lấy thông tin người dùng hiện tại
    this.accountService.currentUser$.pipe(take(1)).subscribe(user => this.user= user);
    // lấy giá trị đầu tiên
  }
  ngOnInit(): void {
    this.initializeUploader();
  }

  fileOverBase(e: any){
    this.hasBaseDropzoneOver = e;

  }
  setMainPhoto(photo: Photo){
    this.memberService.setMainPhoto(photo.id).subscribe(()=>{
      this.user.photoUrl= photo.url;
      this.accountService.setCurrentUser(this.user);
      this.member.photoUrl= photo.url;
      this.member.photos.forEach(p => {
        if(p.isMain)p.isMain= false;
        if(p.id== photo.id) p.isMain= true;
      });

    })
    // đây là pth trong tyscript nó được gọi để đăt ảnh địa diện hcinsh cho gn dùng
    // với đối số đầu vào là 1 đối tuowngh photo, đại diện cho ahr mà ng dùng đã chọn làm ảnh địa điẹn


  }
  deletePhoto(photoId: number){
    this.memberService.deletePhoto(photoId).subscribe(()=>{
      this.member.photos = this.member.photos.filter(x => x.id!= photoId);

    })
  }
  //gọi đến 1 service thwucj hiện 1 pt detelephoto , nó lọc thành viên trong ảnh và chỉ giữ lại ảnh có id khác với photoid đã truyền vào
  // tucwcs là nó sẽ xóa ảnh k trùng với photoid


  // tạo hàm khởi tạo upload
  initializeUploader(){
    // cấu hình danh sách
    this.uploader = new FileUploader({
      url: this.baseUrl + 'users/add-photo',
      authToken: 'Bearer ' + this.user.token,
      isHTML5: true, // trình duyệt có hỗ trợ html5
      allowedFileType:['image'],  // 1 mảng chưa loại tệp đc cho phép
      removeAfterUpload:true,
      autoUpload: false,
      maxFileSize: 10*1023*1024

    });

    this.uploader.onAfterAddingFile= (file)=>{
      // file.withCredentials = true;
      file.withCredentials = false;


    }
    //đc kích hoạt khi 1 hoặc nhiều tệp đc thêm vào hàng đợi đê tải lên




    // this.uploader.onBeforeUploadItem = (item) => {
    //   // console.log(item);
    // };

    // this.uploader.onProgressItem = (file, progress) => {
    //   // console.log(fileItem, progress);
    // };

    this.uploader.onSuccessItem = (item, response, status, headers) => {
       console.log(item, response, status, headers);
      //trả lại trạng thái
      // để sử lý phản hồi về cho máy chủ
      if(response){
        const photo = JSON.parse(response);
        this.member.photos.push(photo);
      }
    };

  }

}


// trong component này nó sẽ nhnhanathanhf phần của thành viên
