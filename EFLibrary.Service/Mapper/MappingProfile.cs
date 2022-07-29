using AutoMapper;
using EFLibrary.Domain.Entities.Books;
using EFLibrary.Domain.Entities.Employes;
using EFLibrary.Domain.Entities.Libraries;
using EFLibrary.Domain.Entities.Orders;
using EFLibrary.Domain.Entities.Users;
using EFLibrary.Service.DTOs.BookDTOs;
using EFLibrary.Service.DTOs.EmployeeDTOs;
using EFLibrary.Service.DTOs.LibraryDTOs;
using EFLibrary.Service.DTOs.OrderDTOs;
using EFLibrary.Service.DTOs.UserDTOs;


namespace EFLibrary.Service.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserForCreationDto>().ReverseMap();
            CreateMap<Book, BookForCreationDto>().ReverseMap();
            CreateMap<Library, LibraryForCreationDto>().ReverseMap();
            CreateMap<Employee, EmployeeForCreationDto>().ReverseMap();
            CreateMap<Order, OrderForCreationDto>().ReverseMap();
        }
        
    }
}