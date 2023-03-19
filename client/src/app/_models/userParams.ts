import { User } from "./user";

export  class UserParams {
  gender: string;
  minAge = 18;
  maxAge = 99;
  pageNumber =1;
  pageSize = 5;


  constructor(user: User) {
    this.gender = user.gender === 'female' ? 'male' : 'female';
  }
}

// sau khi đã tạo đc thuộc tính này chỉ ở memberservice gọi nó đne s

