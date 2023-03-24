using API.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Data
{
    
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<AppUser> Users { get; set; }

        public DbSet<UserLike> Likes { get; set; }
        // đại diện cho tập hợp các đối tượng userlike đc luuw trong cơ sở dữ liệu
        // cung cấp các pth để thực hiện các thao tac CRUD

        public DbSet<Message> Messages { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<UserLike>()
               .HasKey(k => new {k.SourceUserId, k.LikedUserId}); // khóa chính của bảng


            builder.Entity<UserLike>()
                .HasOne(s => s.SourceUser)
                .WithMany(l => l.LikedUsers)
                .HasForeignKey(s=>s.SourceUserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<UserLike>()
                .HasOne(s => s.LikedUser)
                .WithMany(l => l.LikeByUsers)  // sử dụng quan hệ 1 nhiều ( 1 ng dùng có thể thích đc nhiều ng khác)
                .HasForeignKey(s=>s.LikedUserId)
                .OnDelete(DeleteBehavior.Cascade); //chỉ định hành động xóa liên lục
            
            builder.Entity<Message>()
                .HasOne(u=> u.Recipient)   // xác định 1 đối tượng Message chỉ có 1 đối tượng Recipinet ng nhận 
                .WithMany(m => m.MessagesReceived)
                .OnDelete(DeleteBehavior.Restrict);
                //
            builder.Entity<Message>()
                .HasOne(u=> u.Sender) 
                .WithMany(m => m.MessagesSent)
                .OnDelete(DeleteBehavior.Restrict); //xác định hành động đc thực hiện khi đối tượng liên quan được xóa  
            
        }
        // pth này dùng để cấu hình mô hình dữ liêu
        // cấu hình 1 số quan hệ giữa các bảng trong cở sở dữ liệu và sử dụng Fluent APi để đĩnh nghãi các thuộc tính trong bảng 
        //Fluent API là pp cấu hình các quan hệ giữa các đối tượng trở nên rõ ràng linh hoạt so với viejc sự dụng annotation


    }
}
// đại diện cho 1 đối tượng kết nối cơ sở dữ liệu
//pth này dùng để cấu hình dữ liệu và xác định quan hệ giữa các bảng trong cơ sở dữ liệu 






















/*
Các dòng code trong phương thức OnModelCreating sẽ cấu hình một số quan hệ giữa các bảng trong cơ sở dữ liệu
và sử dụng Fluent API để định nghĩa các thuộc tính của bảng.

Cụ thể, dòng đầu tiên base.OnModelCreating(builder); 
gọi phương thức cơ sở của lớp cha OnModelCreating,
để bảo đảm rằng tất cả các cấu hình cơ bản của Entity Framework được thực hiện.

Dòng tiếp theo builder.Entity<UserLike>().HasKey(k => new {k.SourceUserId, k.LikedUserId});
định nghĩa rằng bảng UserLike có khóa chính được tạo từ cột SourceUserId và LikedUserId.
Điều này cho phép Entity Framework map các đối tượng UserLike trong ứng dụng C# với các bản ghi trong bảng tương ứng trong cơ sở dữ liệu.

Dòng tiếp theo builder.Entity<UserLike>().HasOne(s => s.SourceUser).WithMany(l => l.LikeUsers).HasForeignKey(s=>s.SourceUserId).OnDelete(DeleteBehavior.Cascade); 
định nghĩa mối quan hệ giữa bảng UserLike và bảng AppUser thông qua thuộc tính SourceUser của lớp UserLike.
 Điều này cho phép Entity Framework map một đối tượng UserLike với một đối tượng AppUser trong cơ sở dữ liệu.
Mối quan hệ được thiết lập là "một nhiều" (một người dùng có thể thích nhiều người dùng khác), 
được định nghĩa bởi phương thức WithMany. Thuộc tính LikeUsers trên đối tượng AppUser sẽ lưu trữ tất cả các UserLike mà người dùng đó đã thích. Phương thức HasForeignKey được sử dụng để định nghĩa khóa ngoại của mối quan hệ giữa hai bảng và OnDelete để thiết lập hành động khi có sự xóa dữ liệu bảng AppUser.
*/
