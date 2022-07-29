using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using EFLibrary.Domain.Commons;
using EFLibrary.Domain.Entities.Books;
using EFLibrary.Domain.Entities.Users;
using EFLibrary.Service.DTOs.UserDTOs;

namespace EFLibrary.Service.Interfaces
{
    public interface IUserService 
    {
        
        Task<BaseResponse<User>> CreateAsync(UserForCreationDto entity);

        Task<BaseResponse<User>> UpdateAsync(long id, UserForCreationDto entity);

        Task<BaseResponse<bool>> DeleteAsync(Expression<Func<User, bool>> expression);

        Task<BaseResponse<User>> GetAsync(Expression<Func<User, bool>> expression);

        Task<BaseResponse<IEnumerable<User>>> GetAllAsync(Expression<Func<User, bool>> expression = null, Tuple<int, int> pagination = null);

        Task<BaseResponse<bool>> CheckLoginAsync(string username, string password);
        Task<BaseResponse<User>> ChangePasswordAsync(UserForChangePassword forChangePassword);
       
    }
}