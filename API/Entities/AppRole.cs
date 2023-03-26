using System.Collections;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace API.Entities
{
    public class AppRole : IdentityRole<int>
    {
        public ICollection<AppUserRole> UserRoles { get; set; }
        // sao chép cái này để thêm vào AppUser

        
    }
}
// chúng ta có danh sách các vai trò , cho phép người dùng tham gia
// lớp approle là lớp kế thừa từ lớp identityrole để mặc định 1 vai trò trong hệ thống
//lớp identityrole nó chứa 1 vài thông tin cơ bản như id, name, NormalizedName(tiêu chuẩn hóa)
//tham sô kiểu int đc sử dụng để chỉ định kiểu dữ liệu có thuộc tinh id trong lớp identityrole 
//identityrole sử dụng kiểu dữ liệu string cho thuộc tính id 