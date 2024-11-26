using AngularApp1.Server.Entities;

namespace AngularApp1.Server.Repositories.OrderRepo
{
    public interface IOrderRepository : IRepository<OrderMst>
    {
        Task<OrderMst> GetOrderWithDetailsAsync(int orderId);
        Task<(IEnumerable<OrderMst> Orders, int TotalCount)> GetPaginatedOrdersAsync(
            int pageNumber, int pageSize, string customerName = null);

        Task<IEnumerable<OrderMst>> GetOrdersByCustomerAsync(int customerId);
    }
}

