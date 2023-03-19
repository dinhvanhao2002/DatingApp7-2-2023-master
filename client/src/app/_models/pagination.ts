
export interface Pagination {

  currentPage : number;
  itemsPerPage: number;
  totalItems : number;
  totalPages : number;



}

export class PaginatedResult<T>{
  result: T ;
  pagination: Pagination ;
  // đc định nghĩa 1 kết quả trả về đc phân trang trong 1 ứng dụng
  // result địa diện cho kiểu dữ liệu t , đại diện cho loại dữ liệu của kết quả trả về

}


