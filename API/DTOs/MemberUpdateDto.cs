namespace API.DTOs
{
    public class MemberUpdateDto
    {
        // trong phần thay đổi của ứng dụng nó chỉ cần thay đổi 3 trường
        public string Introduction { get; set; }

        public string LookingFor { get; set; }
        public string Interests { get; set; }

        public string City { get; set; }
        public string Country { get; set; }

        


    }
}