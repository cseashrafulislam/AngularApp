using AngularApp1.Server.Entities;

namespace AngularApp1.Server.Repositories.ProductRepo
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<bool> IsProductNameUniqueAsync(string productName);

    }
}
