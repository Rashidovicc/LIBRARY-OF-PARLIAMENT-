using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using EFLibrary.Data.IRepository;
using EFLibrary.Data.Repository;
using EFLibrary.Domain.Commons;
using EFLibrary.Domain.Entities.Books;
using EFLibrary.Domain.Entities.Users;
using EFLibrary.Domain.Enums;
using EFLibrary.Service.DTOs.UserDTOs;
using EFLibrary.Service.Extentions;
using EFLibrary.Service.Interfaces;
using EFLibrary.Service.Mapper;

namespace EFLibrary.Service.Services
{
    public class UserService : IUserService
    {
        private IUserRepository _userRepository;
        private IMapper _mapper;

        public UserService()
        {
            _mapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            }).CreateMapper();

            _userRepository = new UserRepository();
        }
        
       

       
        public async Task<BaseResponse<User>> CreateAsync(UserForCreationDto user)
        {
            var response = new BaseResponse<User>();

            if ((await _userRepository.GetAsync(extity => extity.UserName == user.UserName)) is not null)
            {
                response.Error = new ErrorResponse(400, "User is already exists");

                return response;
            }

            if ((await _userRepository.GetAsync(ex=>ex.Email == user.Email)) is not null)
            {
                response.Error = new ErrorResponse(400, "Email is already exists");

                return response;
            }
            var entityToCreate = _mapper.Map<User>(user);

            entityToCreate.Create();

            response.Data = await _userRepository.CreateAsync(entityToCreate);

            await _userRepository.SaveAsync();

            return response;
        }

        
       

       
        public async Task<BaseResponse<User>> UpdateAsync(long id, UserForCreationDto updatedUser)
        {
            var response = new BaseResponse<User>();
            
            var entityToUpdate = await _userRepository.GetAsync(updated => updated.Id == id);

            if (entityToUpdate is null || entityToUpdate.State == ItemState.Deleted)
            {
                response.Error = new ErrorResponse(404, "User is not found");
                return  response;
            }
            entityToUpdate = _mapper.Map(updatedUser, entityToUpdate);

            entityToUpdate.Update();

            _userRepository.Update(entityToUpdate);

            await _userRepository.SaveAsync();

            return response;
        }

       
        public async Task<BaseResponse<bool>> DeleteAsync(Expression<Func<User, bool>> expression)
        {
            var response = new BaseResponse<bool>();

            response.Data = await _userRepository.DeleteAsync(expression);

            await _userRepository.SaveAsync();

            if (response.Data)
                return response;

            response.Error = new ErrorResponse(404, "User is not found");
            return response;

        }

       
        public async Task<BaseResponse<User>> GetAsync(Expression<Func<User, bool>> expression)
        {
            var response = new BaseResponse<User>();

            response.Data = await _userRepository.GetAsync(expression);

            if (response.Data is not null && response.Data.State != ItemState.Deleted) return response;
                response.Error = new ErrorResponse(404, "User is not found");

            return response;

        }

        public Task<BaseResponse<IEnumerable<User>>> GetAllAsync(Expression<Func<User, bool>> expression = null, Tuple<int, int> pagination = null)
        {
            var response = new BaseResponse<IEnumerable<User>>();

            response.Data = _userRepository.GetAll(expression).Where(client => client.State != ItemState.Deleted).GetWithPagination(pagination);

            return Task.FromResult(response);
        }

        
        public async Task<BaseResponse<bool>> CheckLoginAsync(string username, string password)
        {
            var user = await _userRepository.GetAsync(p => p.UserName == username);

            if (user is null)
            {
                return new BaseResponse<bool>
                {
                    Error = new ErrorResponse(404, "User is not found")
                };
            }

            if (user.Password != password.GetHash())
            {
                return new BaseResponse<bool>
                {
                    Data = false,
                    Error = new ErrorResponse(404, "Password is not correct")
                };

            }

            return new BaseResponse<bool>
            {
                Data = true,
            };
        }
       
       
        public async Task<BaseResponse<User>> ChangePasswordAsync(UserForChangePassword forChangePassword)
        {
            var response = new BaseResponse<User>();

            var oldUser = await _userRepository.GetAsync(user => user.UserName == forChangePassword.UserName);

            if (oldUser is null)
            {
                response.Error = new ErrorResponse(404, "User is not found");

                return response;
            }

            if (oldUser.Password != forChangePassword.OldPassword.GetHash())
            {
                response.Error = new ErrorResponse(404, "Password is not correct");
                return response;
            }

            if (forChangePassword.NewPassword != forChangePassword.ConfirmPassword)
            {
                response.Error = new ErrorResponse(404, "New password and confirm password are not equal");

                return response;
            }

            oldUser.Password = forChangePassword.NewPassword.GetHash();
            oldUser.UpdatedAt = DateTime.UtcNow;

            _userRepository.Update(oldUser);
            await _userRepository.SaveAsync();

            response.Data = oldUser;

            return response;
        }

       
    
      
    }
}