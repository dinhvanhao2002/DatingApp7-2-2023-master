using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Helpers;

namespace API.Interfaces
{
    public interface ILikesRepository
    {
        // tạo tác vụ tra rveef lượt thích của ng dùng
        Task<UserLike> GetUserLike(int sourceUserId, int likedUserId);
        //nhận lượt thích của ng dùng
        // lấy thông tin về 1 lượt thích về ng dùng


        Task<AppUser> GetUserWithLikes(int userId);
        // pth lấy thông tin của 1 ng dùng cùng với danh sách lượt thích của ng dùng đó

        Task<PagedList<LikeDto>> GetUserLikes(LikesParams likesParams);
        // pth lấy danh sách lượt thích của ng dùng

    
         
    }
}