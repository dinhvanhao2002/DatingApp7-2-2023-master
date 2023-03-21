using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Extensions;
using API.Helpers;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class LikesRepository : ILikesRepository
    {
        private readonly DataContext _context;
        public LikesRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<UserLike> GetUserLike(int sourceUserId, int likedUserId)
        {
            return await _context.Likes.FindAsync(sourceUserId, likedUserId);
        }

        public async Task<PagedList<LikeDto>> GetUserLikes(LikesParams  likesParams)
        {
             var users = _context.Users.OrderBy(u => u.UserName).AsQueryable();  //lấy tất cả thông tin của ng dùng và sắp xếp theo tên của ng dùng
             var likes = _context.Likes.AsQueryable();  // lấy danh sách các lượt thích trong cơ sở dữ liệu
             if(likesParams.Predicate =="liked")
             {
                likes = likes.Where(like=>like.SourceUserId ==likesParams.UserId);  // lọc danh sách các lượt thích
                users = likes.Select(like=> like.LikedUser);

             }
             if(likesParams.Predicate == "likedBy")
             {
                likes = likes.Where(like =>like.LikedUserId == likesParams.UserId);
                users = likes.Select(like => like.SourceUser);
             }

             var likedUsers = users.Select(user => new LikeDto{
                Username= user.UserName,
                KnownAs= user.KnownAs,
                Age = user.DateOfBirth.CalculateAge(),
                PhotoUrl = user.Photos.FirstOrDefault(p=>p.IsMain).Url,
                City = user.City,
                Id = user.Id
             });

             return await PagedList<LikeDto>.CreateAsync(likedUsers,likesParams.PageNumber, likesParams.PageSize);

             
        }
        //khai triển danh sách lượt thích của ng dùng

        public async Task<AppUser> GetUserWithLikes(int userId)
        {
            return await _context.Users  // truy cập tới bảng user trong cơ sở dữ liệu thông qua đối tượng context
            .Include(x=>x.LikedUsers)    //thêm danh sách các ng dùng đã thích
            .FirstOrDefaultAsync(x => x.Id == userId);   //thục hiện truy vấn lấy thông tin ng dùng
        
        }
        //sử dụng lấy thông tin của 1 ng dùng cùng với danh sách của ng dùng mà ng đó đã thích 
    }
}