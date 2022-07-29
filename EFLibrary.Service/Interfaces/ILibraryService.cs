using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using EFLibrary.Domain.Commons;
using EFLibrary.Domain.Entities.Libraries;
using EFLibrary.Service.DTOs.LibraryDTOs;

namespace EFLibrary.Service.Interfaces
{
    public interface ILibraryService 
    {
        Task<BaseResponse<Library>> CreateAsync(LibraryForCreationDto entity);

        Task<BaseResponse<Library>> UpdateAsync(long? id, LibraryForCreationDto entity);

        Task<BaseResponse<bool>> DeleteAsync(Expression<Func<Library, bool>> expression);

        Task<BaseResponse<Library>> GetAsync(Expression<Func<Library, bool>> expression);

        Task<BaseResponse<IEnumerable<Library>>> GetAllAsync(Expression<Func<Library, bool>> expression = null, Tuple<int, int> pagination = null);

        
    }
}