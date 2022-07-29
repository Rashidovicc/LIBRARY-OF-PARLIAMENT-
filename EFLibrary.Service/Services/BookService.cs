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
using EFLibrary.Domain.Enums;
using EFLibrary.Service.DTOs.BookDTOs;
using EFLibrary.Service.Extentions;
using EFLibrary.Service.Interfaces;
using EFLibrary.Service.Mapper;

namespace EFLibrary.Service.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;
        public BookService()
        {
           _bookRepository = new BookRepository();
           _mapper = new MapperConfiguration(cfg =>
           {
               cfg.AddProfile<MappingProfile>();
           }).CreateMapper();

        }


        public async Task<BaseResponse<Book>> CreateAsync(BookForCreationDto entity)
        {
            var response = new BaseResponse<Book>();

            
            var entityToCreate = _mapper.Map<Book>(entity);

            entityToCreate.Create();

            response.Data = await _bookRepository.CreateAsync(entityToCreate);

            await _bookRepository.SaveAsync();

            return response;
        }

        public async Task<BaseResponse<Book>> UpdateAsync(long id, BookForCreationDto entity)
        {
            var response = new BaseResponse<Book>();

            var entityToUpdate = await _bookRepository.GetAsync(e => e.Id == id);

            if (entityToUpdate is null || entityToUpdate.State == ItemState.Deleted)
            {
                response.Error = new ErrorResponse(404, "Book is not found for updating");

                return response;
            }

            entityToUpdate = _mapper.Map(entity, entityToUpdate);

            entityToUpdate.Update();

            _bookRepository.Update(entityToUpdate);

            await _bookRepository.SaveAsync();

            return response;
        }

        public async Task<BaseResponse<bool>> DeleteAsync(Expression<Func<Book, bool>> expression)
        {
            var response = new BaseResponse<bool>();

            var entity = await _bookRepository.GetAsync(expression);

            if (entity is null || entity.State == Domain.Enums.ItemState.Deleted)
            { 
                response.Error = new ErrorResponse(400, "Book is not exists");
                response.Data = false;

                return response;
            }

            entity.Delete();

            await _bookRepository.SaveAsync();

            response.Data = true;

            return response;
        }

        public async Task<BaseResponse<Book>> GetAsync(Expression<Func<Book, bool>> expression)
        {
            var response = new BaseResponse<Book>();

            response.Data = await _bookRepository.GetAsync(expression);

            if (response.Data is null || response.Data.State == ItemState.Deleted)
            {
                response.Error = new ErrorResponse(404, "Book is not found");

                return response;
            }

            return response;
        }

        public Task<BaseResponse<IEnumerable<Book>>> GetAllAsync(Expression<Func<Book, bool>> expression = null, Tuple<int, int> pagination = null)
        {
            var response = new BaseResponse<IEnumerable<Book>>();

            response.Data = _bookRepository.GetAll(expression)
                .Where(r => r.State != Domain.Enums.ItemState.Deleted)
                .GetWithPagination(pagination);

            return Task.FromResult(response);
        }
    }
}