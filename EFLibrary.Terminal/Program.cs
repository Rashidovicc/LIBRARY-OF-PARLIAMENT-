using System;
using System.Threading.Tasks;
using EFLibrary.Data.IRepository;
using EFLibrary.Domain.Entities.Books;
using EFLibrary.Domain.Entities.Libraries;
using EFLibrary.Domain.Enums;
using EFLibrary.Service.DTOs.BookDTOs;
using EFLibrary.Service.DTOs.EmployeeDTOs;
using EFLibrary.Service.DTOs.LibraryDTOs;
using EFLibrary.Service.DTOs.OrderDTOs;
using EFLibrary.Service.DTOs.UserDTOs;
using EFLibrary.Service.Interfaces;
using EFLibrary.Service.Services;

namespace EFLibrary.Terminal
{
    class Program
    {
        static Task Main(string[] args)
        {
            Library library = new Library();
            ILibraryService libraryService = new LibraryService();
            IBookService bookService = new BookService();
            IUserService userService = new UserService();
            IEmployeeService employeeService = new EmployeeService();
            IOrderService orderService = new OrderService();
            //var res = orderService.GetAsync(e => e.Id == 1);

            //Console.WriteLine(res.Result.Data.User.FullName);

            var chiq = userService.CreateAsync(new UserForCreationDto()
            {
                FullName = "Ahmad Umarov",
                UserName = "Ahadder",
                Email = "salommenawdf",
                Password = "salom1202",
                Address = "qatortol",
                Phone = "99898989",
            });

            Console.WriteLine(chiq.Result.Error.Error);
            return Task.CompletedTask;
        }
    }
}
