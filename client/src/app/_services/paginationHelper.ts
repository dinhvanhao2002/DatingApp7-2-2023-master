import { HttpClient, HttpParams } from '@angular/common/http';
import { map } from 'rxjs';
 import { PaginatedResult } from '../_models/pagination';



 export function getPaginatedResult<T>(url:string,params: any, http:HttpClient ) {
    const paginatedResult : PaginatedResult<T> = new PaginatedResult<T>();
    // tạo 1 đối tượng paginated result và trả về 1 lớp paginated result

    // gọi pth get trên đối tượng http, với tham số url
    //pth pipe sẽ trả về 1 luồng dữ liệu obsererable để xử lý dữ liệu trả về
    // pht map ddeec gọi để xử lý phản hồi từ điểm cuối api

    return http.get<T>(url, { observe: 'response', params }).pipe(
      map(response => {
        if(response.body!=null  && response.body !== undefined)
        {
          paginatedResult.result = response.body;
        }
        //kiểm tra phản hồi

        if (response.headers.get('Pagination') !== null) {
          paginatedResult.pagination = JSON.parse(response.headers.get('Pagination') || '{}');
          console.log(paginatedResult.result);
        }
        return paginatedResult;
      }),
      //map(paginatedResult => paginatedResult)
    );
  }
  //hầm typescript để lấy kết quả phân trang từ 1 điểm cuối api




  export function getPaginationHeaders(pageNumber: number, pageSize: number)
  {
    let params = new HttpParams();
    //tạo 1 đối tượng params mới từ lớp httpparams đc sử dụng để chứa các tham số truy vấn
    params = params.append('pageNumber', pageNumber.toString());
    params = params.append('pageSize', pageSize.toString());
    // thêm tham số truy vấn pageNumber và pageSize vào đối tượng params , với giá trị là số trang hiện tịa đc chuyển đổi thành chuỗi

    return params;
    // sau đó đối tượng params chuwasc ác tham số truy vấn đc tạo
  }

  // tạo 1 file mới có chức năng phân trang trong angular bằng cách cung cấp các hàm helper để lấy thông tin phân trang từ httpheaders và gửi yêu cầu http để lấy các trang dữ liệu phù hợp với yêu cầu phân trang

