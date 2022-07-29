using EFLibrary.Data.IRepository;
using EFLibrary.Domain.Entities.Users;

namespace EFLibrary.Data.Repository
{
    public class UserRepository : GenericRepository<User>,IUserRepository
    {
        
    }
}