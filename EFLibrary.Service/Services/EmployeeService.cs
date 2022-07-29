using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using EFLibrary.Data.IRepository;
using EFLibrary.Data.Repository;
using EFLibrary.Domain.Commons;
using EFLibrary.Domain.Entities.Employes;
using EFLibrary.Domain.Entities.Users;
using EFLibrary.Domain.Enums;
using EFLibrary.Service.DTOs.EmployeeDTOs;
using EFLibrary.Service.Extentions;
using EFLibrary.Service.Interfaces;
using EFLibrary.Service.Mapper;

namespace EFLibrary.Service.Services
{
    public class EmployeeService : IEmployeeService
    {
        private IEmployeeRepository _employeeRepository;
        private IMapper _mapper;

        public EmployeeService()
        {
            _mapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            }).CreateMapper();

            _employeeRepository = new EmployeeRepository();
        }


        public async Task<BaseResponse<Employee>> CreateAsync(EmployeeForCreationDto entity)
        {
            var response = new BaseResponse<Employee>();

            var oldEntity = await _employeeRepository.GetAsync(p => p.UserName == entity.UserName);

            if (oldEntity is not null)
            {
                if (oldEntity.State != ItemState.Deleted &&
                    oldEntity.UserName == entity.UserName &&
                    oldEntity.Password == entity.Password.GetHash())
                {
                    oldEntity.State = ItemState.Updated;

                    _employeeRepository.Update(oldEntity);
                    
                    await _employeeRepository.SaveAsync();

                    response.Data = oldEntity;

                    return response;
                }

                response.Error = new ErrorResponse(400, "Employee is already exists. Enter with Login");

                return response;
            }


            var entityToCreate = _mapper.Map<Employee>(entity);
            entityToCreate.Password = entityToCreate.Password.GetHash();

            entityToCreate.Create();

            response.Data = await _employeeRepository.CreateAsync(entityToCreate);
            
            await _employeeRepository.SaveAsync();

            return response;

        }

        public async Task<BaseResponse<Employee>> UpdateAsync(long id, EmployeeForCreationDto entity)
        {
            var response = new BaseResponse<Employee>();

            var entityToUpdate = await _employeeRepository.GetAsync(entit => entit.Id == id);

            if (entityToUpdate is null || entityToUpdate.State == ItemState.Deleted)
            {
                response.Error = new ErrorResponse(404, "Employee is not found");

                return response;
            }

            entityToUpdate = _mapper.Map(entity, entityToUpdate);

            entityToUpdate.Update();

            _employeeRepository.Update(entityToUpdate);

            await _employeeRepository.SaveAsync();

            return response;
        }

        public async Task<BaseResponse<bool>> DeleteAsync(Expression<Func<Employee, bool>> expression)
        {
            var response = new BaseResponse<bool>();

            var entity = await _employeeRepository.GetAsync(expression);

            if (entity is null)
            {
                response.Error = new ErrorResponse(400, "Emplyee is not exists");
                response.Data = false;

                return response;
            }

            entity.Delete();

            await _employeeRepository.SaveAsync();

            response.Data = true;

            return response;
        }

        public async Task<BaseResponse<Employee>> GetAsync(Expression<Func<Employee, bool>> expression)
        {
            var response = new BaseResponse<Employee>();

            response.Data = await _employeeRepository.GetAsync(expression);

            if (response.Data is null || response.Data.State == ItemState.Deleted)
            {
                response.Error = new ErrorResponse(404, "Employee is not found");

                return response;
            }

            return response;
        }

        public Task<BaseResponse<IEnumerable<Employee>>> GetAllAsync(Expression<Func<Employee, bool>> expression = null, Tuple<int, int> pagination = null)
        {
            var response = new BaseResponse<IEnumerable<Employee>>();

            response.Data = _employeeRepository.GetAll(expression).Where(emp => emp.State != ItemState.Deleted).GetWithPagination(pagination);

            return Task.FromResult(response);
        }

        public async Task<BaseResponse<bool>> CheckIsAlreadyExists(EmployeeLoginDto login)
        {
            var response = new BaseResponse<bool>();
            var entity = await _employeeRepository.GetAsync
                (sourse => sourse.UserName == login.UsernameOrEmail || sourse.Email== login.UsernameOrEmail && sourse.State != ItemState.Deleted);

            if (entity is null)
            {
                response.Error = new ErrorResponse(404, "Employee not found");
                response.Data = false;

                return response;
            }

            if (entity.Password != login.ConfirmPassword)
            {
                response.Error = new ErrorResponse(400, "password is incorrect");
                response.Data = false;

                return response;
            }

            response.Data = true;

            return response;        }

        public async Task<BaseResponse<Employee>> ChangePassword(EmployeeForChangePassword details)
        {
            var response = new BaseResponse<Employee>();

            var entity = await _employeeRepository.GetAsync
                (en => en.UserName == details.UsernameOrEmail || en.Email == details.UsernameOrEmail && en.State != ItemState.Deleted);

            if (entity is null)
            {
                response.Error = new ErrorResponse(404, "Employee not found");

                return response;
            }

            if (entity.Password != details.OldPassword)
            {
                response.Error = new ErrorResponse(400, "Old password is incorrect");

                return response;
            }

            entity.Password = details.NewPassword.GetHash();
            entity.Update();

            
            response.Data = _employeeRepository.Update(entity);

            await _employeeRepository.SaveAsync();

            return response;

        }
    }
}