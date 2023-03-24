using System;

namespace API.Extensions
{
    public static class DateTimeExtensions
    {
        public static int CalculateAge( this DateTime dob){ // dob đại diện cho ngày sinh của 1 người
            var today = DateTime.Today;

            var age = today.Year - dob.Year;

            if (dob > today.AddYears(-age)) age--;

            return age;
            


        }
    }
}

// đây là pth mở rộng Datetime, yêu cầu ng dùng phải trên 18 tuổi 