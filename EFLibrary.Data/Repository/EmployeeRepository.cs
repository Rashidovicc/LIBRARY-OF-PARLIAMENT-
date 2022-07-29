using EFLibrary.Data.IRepository;
using EFLibrary.Domain.Entities.Employes;

namespace EFLibrary.Data.Repository
{
    public class EmployeeRepository : GenericRepository<Employee>,IEmployeeRepository
    {
        
    }
}