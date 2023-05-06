namespace API.Entities
{
    public class UserLike
    {
        public AppUser SourceUser { get; set; }
        // đại diện cho ng dùng mã đã thực hiện hành động like

        public int SourceUserId { get; set; }
        // đại diện cho id của ng dùng đã thực hiện hành động like

        public AppUser LikedUser { get; set; }
        // đại diện cho ng dũng đã được like

        public int LikedUserId { get; set; }
        // đại diện cho id của ng dùng đã được like
        
    }
}

// sau đó cấu hình thực thể trong Datacontext