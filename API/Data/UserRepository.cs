using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Helpers;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public UserRepository(DataContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public Task DeletePhotoAsync(string publicId)
        {
            throw new System.NotImplementedException();
        }

        public async Task<MemberDto> GetMemberAsync(string username)
        {
            return await _context.Users
              .Where(x => x.UserName == username)
              .ProjectTo<MemberDto>(_mapper.ConfigurationProvider)
              .SingleOrDefaultAsync();
        }

        public async Task<PagedList<MemberDto>> GetMembersAsync(UserParams userParams)
        {
            var query = _context.Users.AsQueryable();
            //lấy tất cả các bản ghi trong bảng user của dbcontext

            // .ProjectTo<MemberDto>(_mapper.ConfigurationProvider)
            // .AsNoTracking()
            // .AsQueryable();

            query = query.Where(u=> u.UserName  != userParams.CurrentUsername);
            // mục đích loại bỏ user giống với user ng dùng hiện tại, do là vì mình đã lấy tất cả

            query = query.Where(u => u.Gender!= userParams.Gender);
            var minDob = DateTime.Today.AddYears(-userParams.MaxAge - 1); // tức là độ tuổi của ng dùng trừ đi 1 năm
            var maxDob = DateTime.Today.AddYears(-userParams.MinAge); // tức là độ tuổi tối thiểu của ng dùng 

            query = query.Where(u => u.DateOfBirth >= minDob && u.DateOfBirth <= maxDob); // ngày sinh của ng dùng hiện tại lớn hơn ngày sinh tối thiểu minbox
            // ngày sinh của ng dùng nhỏ hơn ngày sinh tối đa 
            return await PagedList<MemberDto>.CreateAsync(query.ProjectTo<MemberDto>(_mapper.ConfigurationProvider).AsNoTracking(), userParams.PageNumber,userParams.PageSize);


            // lấy cả danh sách 
        }

        public async Task<AppUser> GetUserByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<AppUser> GetUserByUsernameAsync(string username)
        {
            return await _context.Users
                .Include(p => p.Photos)
                .SingleOrDefaultAsync(x => x.UserName == username);
        }

        public Task<ActionResult<AppUser>> GetUserByUsernameAsync()
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<AppUser>> GetUsersAsync()
        {
            //return await _context.Users.ToListAsync();
            return await _context.Users
                 .Include(p => p.Photos)
                 .ToListAsync();
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public void Update(AppUser user)
        {
            _context.Entry(user).State = EntityState.Modified;
        }
    }
}

//dùng để lấy dữ liệu người dùng được lưu trữ 
// cung cấp 1 cách tiêu chuẩn hóa để cập dữ liệu ng dùng trong kho dữ liệu

// dùng để truy xuất và tương tác với cơ sở dữ liệu, cung cấp các pth thực hiện trên dữ liệu ng dùng 