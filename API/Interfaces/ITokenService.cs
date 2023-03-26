using API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Interfaces
{
    public interface ITokenService
    {
        Task<string> CreateToken(AppUser user);
    }
}
//Interfaces  là 1 giao diện trong ứng dụng nó định nghĩa 1 pth createtoken để tạo 1 chuỗi token cho đối tượng Appuser
//tạo token thông qua đối tượng Appuser việc này đc thực hiện thông qua việc tạo 1 đối tượng userdto và sao chép thông tin từ đối tượng appuser vào đối tương userdto để lưu các thuộc tính của appuser
//