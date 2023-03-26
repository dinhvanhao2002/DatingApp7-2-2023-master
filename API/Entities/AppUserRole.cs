using Microsoft.AspNetCore.Identity;

namespace API.Entities
{
    public class AppUserRole : IdentityUserRole<int> // đại diện cho đối tượng kết nối giữa đối tượng identityuser và identityrole
    {
        public AppUser User { get; set; }
        //định nghĩa 1 đối tượng kết nối giữa đối tượng appuser và các đối tượng approle 
        // đói tượng appuserrole chứa 1 thuộc tính user kiểu appuser 
        //đại diện cho ng dùng liên kết với vai trò trong ứng dụng
        //với đối tượng appuserrole ta có thể xác định rằng 1 ng dùng có quyền truy cập 1 vai trog cụ thể nào đó 
        // ta có thể xác định rằng 1 ng dùng có quyền truy cập 1 vai trò cụ thể nào đó trong hệ thống phân quyền , nó đc sử dụng như 1 đối tượng nhiều nhiều
        //ví dụ cụ thể như là ng dùng a có quyền truy cập vào vai trò b , thông qua việc liên kết các đối tượng appuser và approle

        public AppRole Role { get; set; }
        //role biểu diễn các vai trò của đối tượng username



    }
}

// public là 1 phạm vi truy cập công khai , có thể truy cập bất kì tron ứng dụng 
/*
Lớp IdentityUser là một lớp trong ASP.NET Core Identity, cung cấp các thuộc tính và phương thức cơ bản để quản lý thông tin người dùng trong ứng dụng. Lớp này có thể được kế thừa và tùy chỉnh theo nhu cầu của ứng dụng.

Các thuộc tính của lớp IdentityUser bao gồm:

Id: định danh duy nhất của người dùng
UserName: tên đăng nhập của người dùng
NormalizedUserName: tên đăng nhập đã được chuẩn hóa
Email: địa chỉ email của người dùng
NormalizedEmail: địa chỉ email đã được chuẩn hóa
EmailConfirmed: xác nhận địa chỉ email của người dùng
PasswordHash: mật khẩu của người dùng đã được mã hóa
SecurityStamp: dấu hiệu bảo mật được sử dụng để tạo lại các thông tin xác thực
ConcurrencyStamp: dấu hiệu đồng thời được sử dụng để đảm bảo tính toàn vẹn của các thay đổi trong cơ sở dữ liệu
PhoneNumber: số điện thoại của người dùng
PhoneNumberConfirmed: xác nhận số điện thoại của người dùng
TwoFactorEnabled: xác thực hai yếu tố được kích hoạt cho người dùng
Lớp IdentityUser cũng cung cấp các phương thức để xác thực người dùng, quản lý mật khẩu, quản lý thông tin người dùng và quản lý xác thực hai yếu tố. Bằng cách sử dụng lớp IdentityUser, bạn có thể dễ dàng thêm chức năng đăng ký, đăng nhập và quản lý người dùng trong ứng dụng của mình.
*/