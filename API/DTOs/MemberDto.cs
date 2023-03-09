using System;
using System.Collections.Generic;
using API.Entities;

namespace API.DTOs
{
    public class MemberDto
    {
        public int Id { get; set; }

        public string Username { get; set; }

        public string PhotoUrl { get; set; }

        public int Age {get; set; }

        public string KnownAs { get; set; }


        public DateTime DateOfBirth  { get; set; }


        public DateTime Created { get; set; }


        public DateTime LastActive { get; set; } 

        public string Gender { get; set; }

        public string Introduction { get; set; }

        public string LookingFor { get; set; }

        public string Interests { get; set; }

        public string City { get; set; }

        public string Country { get; set; }


        public ICollection<PhotoDto> Photos { get; set; }

    
    }
}
// dto là 1 kiểu dữ liệu đc sử dụng trong việc phân tán , được sử dụng chuyển dữ liệu giữ các tầng khác nhau 

// giúp tách biệt dữ liệu từ  lớp đối tượng ứng dụng và dữ liệu đc truyền tải 
// chuyển tư tầng modl sang view
