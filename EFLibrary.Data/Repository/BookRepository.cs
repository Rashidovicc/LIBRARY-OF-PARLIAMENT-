using EFLibrary.Data.IRepository;
using EFLibrary.Domain.Entities.Books;

namespace EFLibrary.Data.Repository
{
    public class BookRepository : GenericRepository<Book>,IBookRepository
    {
        
    }
}