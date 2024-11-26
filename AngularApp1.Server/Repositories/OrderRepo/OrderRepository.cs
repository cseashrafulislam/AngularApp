using AngularApp1.Server.DB;
using AngularApp1.Server.Entities;
using AngularApp1.Server.Repositories.CustomerRepo;
using Microsoft.EntityFrameworkCore;

namespace AngularApp1.Server.Repositories.OrderRepo
{
    public class OrderRepository : GenericRepository<OrderMst>, IOrderRepository
    {
        private readonly ApplicationDbContext _context;

        public OrderRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        // Fetch orders with their details and customer information
        public async Task<OrderMst> GetOrderWithDetailsAsync(int orderId)
        {
            return await _context.OrderMsts
                .Include(o => o.Customer)
                .Include(o => o.OrderDtls)
                    .ThenInclude(od => od.Product)
                .FirstOrDefaultAsync(o => o.Id == orderId);
        }

        // Fetch paginated orders with details
        public async Task<(IEnumerable<OrderMst> Orders, int TotalCount)> GetPaginatedOrdersAsync(
            int pageNumber, int pageSize, string customerName = null)
        {
            var query = _context.OrderMsts
                .Include(o => o.Customer)
                .Include(o => o.OrderDtls)
                .AsQueryable();

            if (!string.IsNullOrEmpty(customerName))
            {
                query = query.Where(o => o.Customer.CustomerName.Contains(customerName));
            }

            var totalCount = await query.CountAsync();
            var orders = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (orders, totalCount);
        }
        public async Task<IEnumerable<OrderMst>> GetOrdersByCustomerAsync(int customerId)
        {
            return await _context.OrderMsts
                .Include(o => o.OrderDtls)
                .ThenInclude(od => od.Product)
                .Where(o => o.CustomerId == customerId)
                .ToListAsync();
        }
    }


}
