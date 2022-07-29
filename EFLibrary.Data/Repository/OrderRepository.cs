using EFLibrary.Data.IRepository;
using EFLibrary.Domain.Entities.Orders;

namespace EFLibrary.Data.Repository
{
    public class OrderRepository : GenericRepository<Order>,IOrderRepository
    {
        
    }
}