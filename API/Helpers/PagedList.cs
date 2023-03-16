using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using Microsoft.EntityFrameworkCore;

namespace API.Helpers
{
    public class PagedList<T>: List<T> // lớp này đại diện cho 1 danh sách các mục thuộc loại trang
    {
        public PagedList( IEnumerable<T> items, int count, int pageNumber, int pageSize) // đây là hàm khởi tạo Pagelist
        {
            // IEnumerable chứa các mục đc bao gòm cho danh sách phần trang
            //
            CurrentPage = pageNumber;
            TotalPages = (int) Math.Ceiling(count/(double)pageSize);// tính tổng số trangq cần thiết để hiện thị các danh mục trong danh sách , hàm math.Ceiling là hàm trpnf số nguyên gần nhất
            PageSize = pageSize;
            TotalCount = count;
            AddRange(items); //sau đóthêm các mục vào mục pagelist
        }

        public int CurrentPage { get; set; }

        public int TotalPages { get; set; }

        public int PageSize {get; set;}

        public int TotalCount { get; set; }

        public static async Task<PagedList<T>> CreateAsync(IQueryable<T> source, int pageNumber, int pageSize)
        {
            var count = await source.CountAsync(); // đếm số lượng phần tử của nguồn dữ liệu  giá trị đc trả về là tổng số phần tử trong nguồn dữ liệu
            var items = await source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync(); // skip và text để lấy các phần tử trong nguồn dữu liệu
            // pth take để lấy các phần tử cần thiết 
            //tolistAsync() đc gọi để lấy dánh sách các phần tử từ nguồn dữ liệu và trả về chúng dưới dạng 1 danh sách 

            return new PagedList<T>(items, count, pageNumber, pageSize);
            

        }

       
        // phương thức đồng bộ CreateAsync đc thêm vào để tạo 1 teang mới , với 1 nguuoonf dữ liệu đc cung cấp để truy vấn query
        // vì truy vấn này có thể truy xuất dữ liệu từ nguồn và tính toán số lượng trang cần hiện thị
        // sau đó sẽ trả về 1 trang mới và trả về nó cho ng dùng
        // việc sử dụng đồng bộ giúp cho ứng dụng của t k bị chặn trong quá trinh truy suất dữ liệu từ nguồn đồng bộ hóa 




    }
}