using AngularApp1.Server.DB;
using AngularApp1.Server.Entities;
using AngularApp1.Server.Repositories.CustomerRepo;

namespace AngularApp1.Server.Repositories.OrderRepo
{
    public class OrderDtlsRepository : GenericRepository<OrderDtls>, IOrderDtlsRepository
    {
        public OrderDtlsRepository(ApplicationDbContext context) : base(context)
        {
        }

    }
}
