using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using EFLibrary.Data.IRepository;
using EFLibrary.Data.Repository;
using EFLibrary.Domain.Commons;
using EFLibrary.Domain.Entities.Orders;
using EFLibrary.Domain.Enums;
using EFLibrary.Service.DTOs.OrderDTOs;
using EFLibrary.Service.Extentions;
using EFLibrary.Service.Interfaces;
using EFLibrary.Service.Mapper;
using Microsoft.EntityFrameworkCore;

namespace EFLibrary.Service.Services
{
    public class OrderService : IOrderService
    {
        private IOrderRepository _orderRepository;
        private IMapper _mapper;

        public OrderService()
        {
          
            _mapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            }).CreateMapper();
            
            _orderRepository = new OrderRepository();
        }

        public async Task<BaseResponse<Order>> CreateAsync(OrderForCreationDto entity)
        {
            var response = new BaseResponse<Order>();
            
            var entityToCreate = _mapper.Map<Order>(entity);

            entityToCreate.Create();

            response.Data = await _orderRepository.CreateAsync(entityToCreate);

            await _orderRepository.SaveAsync();

            return response;
        }

        public async Task<BaseResponse<Order>> UpdateAsync(long id, OrderForCreationDto entity)
        {
            var response = new BaseResponse<Order>();

            var entityToUpdate = await _orderRepository.GetAsync(or => or.Id == id);

            if (entityToUpdate is null || entityToUpdate.State == ItemState.Deleted)
            {
                response.Error = new ErrorResponse(404, "Order is not found");

                return response;
            }

            entityToUpdate = _mapper.Map(entity, entityToUpdate);
            entityToUpdate.Update();

            response.Data = _orderRepository.Update(entityToUpdate);

            await _orderRepository.SaveAsync();

            return response;
        }

        public async Task<BaseResponse<bool>> DeleteAsync(Expression<Func<Order, bool>> expression)
        {
            var response = new BaseResponse<bool>();
            var entityToDelete = await _orderRepository.GetAsync(expression);

            if (entityToDelete is null)
            {
                response.Error = new ErrorResponse( 400,"Order is not exit");
                response.Data = false;

                return response;
            }
            await _orderRepository.DeleteAsync(expression);
            
            await _orderRepository.SaveAsync();

            return response;
        }

        public Task<BaseResponse<Order>> GetAsync(Expression<Func<Order, bool>> expression)
        {
            var response = new BaseResponse<Order>();

            try
            {
                response.Data = _orderRepository.GetAll(expression)
                    .Include(o => o.User)
                    .Include(o => o.Employee)
                    .Include(o => o.Book)
                    .Include(o => o.Library)
                    .FirstOrDefaultAsync(order => order.State != ItemState.Deleted).Result;

                return Task.FromResult(response);
            }
            catch (Exception e)
            {
                response.Error = new ErrorResponse(400, e.Message);
                return Task.FromResult(response);
            }
        }

        public Task<BaseResponse<IEnumerable<Order>>> GetAllAsync
            (Expression<Func<Order, bool>> expression = null, Tuple<int, int> pagination = null)
        {
            var response = new BaseResponse<IEnumerable<Order>>();

            try
            {
                response.Data = _orderRepository.GetAll(expression)
                    .Where(order => order.State != ItemState.Deleted)
                    .Include(o => o.User)
                    .Include(o => o.Employee)
                    .Include(o => o.Book)
                    .Include(o => o.Library)
                    .GetWithPagination(pagination);

                return Task.FromResult(response);

            }
            catch (Exception e)
            {
                response.Error = new ErrorResponse(400, e.Message);

                return Task.FromResult(response);
            }
        }
    }
}