using AngularApp1.Server.DB;
using AngularApp1.Server.Entities;

namespace AngularApp1.Server.Repositories.CustomerRepo
{
    public class CustomerRepository : GenericRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(ApplicationDbContext context) : base(context)
        {
        }
    
    }
}
