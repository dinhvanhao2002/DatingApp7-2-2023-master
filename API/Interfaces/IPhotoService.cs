using System.Threading.Tasks;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;

namespace API.Interfaces
{
    public interface IPhotoService
    {
         Task<ImageUploadResult> AddPhotoAsync (IFormFile file);
         // pth này nhận đối tượng IFormFile là file ảnh đc tải lên , và tra về đối tưởng Imagaload đc đóng gói trong task
         Task<DeletionResult> DeletePhotoAsync (string publicId);
         //pth này nhận 1 chuỗi publicid pth này nhận vào 1 chuỗi publicid đại diện cho ảnh cần xóa 
         // pth trả về 1 đối tượng deletionresult 


         // cả 2 pth này đều sử lý đồng bộ hóa việc xử lý trong 
    }
}



// interface là cách để tạo hành vi đối tượng triển khai 
// cung cấp 1 cách trừu tượng hóa các chức năng của 1 đối tượng và cho các đối tượng có thể tương tác vs nhau linh hoạt hơn
// khi đăng ký dịch vụ cho phep các thành phần của ứng dụng có thể sử dụng Iphotoservice để gọi các pt triển trai trong photoservice
