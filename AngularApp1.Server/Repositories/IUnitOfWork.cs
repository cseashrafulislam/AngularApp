

using AngularApp1.Server.Repositories.CustomerRepo;
using AngularApp1.Server.Repositories.ProductRepo;

namespace AngularApp1.Server.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IProductRepository Product { get; }
        ICustomerRepository Customer { get; }


        int SaveChanges();
        Task<int> SaveChangesAsync();
        void BeginTransaction();
        void CommitTransaction();
        void RollbackTransaction();
    }
}
