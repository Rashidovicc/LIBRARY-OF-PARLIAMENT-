using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using EFLibrary.Domain.Commons;
using EFLibrary.Domain.Entities.Books;
using EFLibrary.Service.DTOs.BookDTOs;

namespace EFLibrary.Service.Interfaces
{
    public interface IBookService 
    {
        Task<BaseResponse<Book>> CreateAsync(BookForCreationDto entity);

        Task<BaseResponse<Book>> UpdateAsync(long id, BookForCreationDto entity);

        Task<BaseResponse<bool>> DeleteAsync(Expression<Func<Book, bool>> expression);

        Task<BaseResponse<Book>> GetAsync(Expression<Func<Book, bool>> expression);

        Task<BaseResponse<IEnumerable<Book>>> GetAllAsync(Expression<Func<Book, bool>> expression = null, Tuple<int, int> pagination = null);

        
    }
}