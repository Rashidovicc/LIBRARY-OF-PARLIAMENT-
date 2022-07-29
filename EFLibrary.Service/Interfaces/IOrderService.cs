using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using EFLibrary.Domain.Commons;
using EFLibrary.Domain.Entities.Orders;
using EFLibrary.Service.DTOs.OrderDTOs;

namespace EFLibrary.Service.Interfaces
{
    public interface IOrderService 
    {
        Task<BaseResponse<Order>> CreateAsync(OrderForCreationDto entity);

        Task<BaseResponse<Order>> UpdateAsync(long id, OrderForCreationDto entity);

        Task<BaseResponse<bool>> DeleteAsync(Expression<Func<Order, bool>> expression);

        Task<BaseResponse<Order>> GetAsync(Expression<Func<Order, bool>> expression);

        Task<BaseResponse<IEnumerable<Order>>> GetAllAsync(Expression<Func<Order, bool>> expression = null, Tuple<int, int> pagination = null);

    }
}