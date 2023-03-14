﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs
{
    public class UserDTO
    {
        public string Username { get; set; }

        public string Token { get; set; }

        public string PhotoUrl { get; set; }
        // sau đó sử lý tại accountcontroller để lấy ảnh 
    }
}


// lớp DTo này đc dùng để về dữ liệu về ng dùng api
// khi ng dùng đăng nhập vào ứng dụng , server api sẽ tạo 1 đối tượng dt0 trả về cho máy khách 

// tóm lại userdto ddc sử dụng để truyền dữ liệu về ng dùng giữa server api và phía máy khách của uwsg dugn