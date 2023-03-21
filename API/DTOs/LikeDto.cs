namespace API.DTOs
{
    public class LikeDto
    {
        public int Id { get; set; }

        public string Username { get; set; }

        public int Age { get; set; }

        public string KnownAs { get; set; }


        public string PhotoUrl { get; set; }

        public string City { get; set; }

        
    }
}

// dùng để trả về dữ liệu ng dùng trong api
// chúng ta có thể lây dữ liệu từ đây 