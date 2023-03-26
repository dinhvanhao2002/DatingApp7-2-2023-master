using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Extensions;
using Microsoft.AspNetCore.Identity;

namespace API.Entities
{
    public class AppUser : IdentityUser<int>
    {
        // public int Id { get; set; }

        // public string UserName { get; set; }

        // public byte[] PasswordHash { get; set; }

        // public byte[] PasswordSalt { get; set; }
        // khi thêm IndentityUser thì nó sẽ xuất hiện cảnh báo
        // id đang cố ghi đè lên triển khai


        public DateTime DateOfBirth  { get; set; }

        public string KnownAs { get; set; }

        public DateTime Created { get; set; } = DateTime.Now;


        public DateTime LastActive { get; set; } = DateTime.Now;

        public string Gender { get; set; }

        public string Introduction { get; set; }

        public string LookingFor { get; set; }

        public string Interests { get; set; }

        public string City { get; set; }

        public string Country { get; set; }


        public ICollection<Photo> Photos { get; set; }

        public ICollection<UserLike> LikeByUsers { get; set; }
        // đại diện cho bộ sưu tập các đối tượng trong UserLike , cho biết ng dùng nào đã thích hiện tịa 
        // đc sử dụng để lưu trữ 1 tập hợp các đối tượng userlike 
        //thuộc tính userlike cho biết tất cả các ng dùng đã thcihs ng dùng hiện tại
        public ICollection<UserLike> LikedUsers { get; set; }
        // thuộc tính này đại diện cho 1 bộ sưu tập các đối tượng trong userlike
        // đại diện cho 1 ng dùng đã thực hiện like trên 1 ng dùng

        public ICollection<Message> MessagesSent { get; set; }

        public ICollection<Message> MessagesReceived { get; set; }


        public ICollection<AppUserRole> UserRoles { get; set; }



    }
}
//nhưng thuộc tính nay sẽ đc thêm bởi migrations 