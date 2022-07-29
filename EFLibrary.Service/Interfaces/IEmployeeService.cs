using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using EFLibrary.Domain.Commons;
using EFLibrary.Domain.Entities.Employes;
using EFLibrary.Domain.Entities.Users;
using EFLibrary.Service.DTOs.EmployeeDTOs;

namespace EFLibrary.Service.Interfaces
{
    public interface IEmployeeService 
    {
        Task<BaseResponse<Employee>> CreateAsync(EmployeeForCreationDto entity);

        Task<BaseResponse<Employee>> UpdateAsync(long id, EmployeeForCreationDto entity);

        Task<BaseResponse<bool>> DeleteAsync(Expression<Func<Employee, bool>> expression);

        Task<BaseResponse<Employee>> GetAsync(Expression<Func<Employee, bool>> expression);

        Task<BaseResponse<IEnumerable<Employee>>> GetAllAsync(Expression<Func<Employee, bool>> expression = null, Tuple<int, int> pagination = null);

        Task<BaseResponse<bool>> CheckIsAlreadyExists(EmployeeLoginDto login);

        Task<BaseResponse<Employee>> ChangePassword(EmployeeForChangePassword details);
    }
}