using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using EFLibrary.Data.IRepository;
using EFLibrary.Data.Repository;
using EFLibrary.Domain.Commons;
using EFLibrary.Domain.Entities.Libraries;
using EFLibrary.Domain.Enums;
using EFLibrary.Service.DTOs.LibraryDTOs;
using EFLibrary.Service.Extentions;
using EFLibrary.Service.Interfaces;
using EFLibrary.Service.Mapper;

namespace EFLibrary.Service.Services
{
    public class LibraryService : ILibraryService
    {
        private ILibraryRepository _libraryRepository;
        private IMapper _mapper;

        public LibraryService()
        {
            _mapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            }).CreateMapper();

            _libraryRepository = new LibraryRepository();
        }


        public async Task<BaseResponse<Library>> CreateAsync(LibraryForCreationDto entity)
        {
            var response = new BaseResponse<Library>();

            if ((await _libraryRepository.GetAsync(e => e.Name == entity.Name)) is not null)
            {
                response.Error = new ErrorResponse(400, "Library is already exists");

                return response;
            }

            var entityToCreate = _mapper.Map<Library>(entity);

            entityToCreate.Create();

            response.Data = await _libraryRepository.CreateAsync(entityToCreate);

            await _libraryRepository.SaveAsync();

            return response;
        }

        public async Task<BaseResponse<Library>> UpdateAsync(long? id, LibraryForCreationDto entity)
        {
            var response = new BaseResponse<Library>();

            var entityToUpdate = await _libraryRepository.GetAsync(lib => lib.Id == id);

            if (entityToUpdate is null || entityToUpdate.State == ItemState.Deleted)
            {
                response.Error = new ErrorResponse(404, "Library is not found for updating");

                return response;
            }

            entityToUpdate = _mapper.Map(entity, entityToUpdate);

            entityToUpdate.Update();

            _libraryRepository.Update(entityToUpdate);

            await _libraryRepository.SaveAsync();

            return response;
        }

        public async Task<BaseResponse<bool>> DeleteAsync(Expression<Func<Library, bool>> expression)
        {
            var response = new BaseResponse<bool>();

            var entity = await _libraryRepository.GetAsync(expression);

            if (entity is null || entity.State == Domain.Enums.ItemState.Deleted)
            { 
                response.Error = new ErrorResponse(400, "Library is not exists");
                response.Data = false;

                return response;
            }

            entity.Delete();

            await _libraryRepository.SaveAsync();

            response.Data = true;

            return response;
        }

        public async Task<BaseResponse<Library>> GetAsync(Expression<Func<Library, bool>> expression)
        {
            var response = new BaseResponse<Library>();
            response.Data = await _libraryRepository.GetAsync(expression);
            
            if(response.Data is null || response.Data.State == ItemState.Deleted)
            {
                response.Error = new ErrorResponse(404, "Library is not found");
                return response;
            }

            return response;
        }

        public Task<BaseResponse<IEnumerable<Library>>> GetAllAsync(Expression<Func<Library, bool>> expression = null, Tuple<int, int> pagination = null)
        {
            var response = new BaseResponse<IEnumerable<Library>>();

            response.Data = _libraryRepository.GetAll(expression)
                .Where(lib => lib.State != ItemState.Deleted)
                .GetWithPagination(pagination);

            return Task.FromResult(response);
        }
    }
}