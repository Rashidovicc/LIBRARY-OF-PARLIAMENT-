using EFLibrary.Data.IRepository;
using EFLibrary.Domain.Entities.Libraries;

namespace EFLibrary.Data.Repository
{
    public class LibraryRepository : GenericRepository<Library> , ILibraryRepository
    {
        
    }
}