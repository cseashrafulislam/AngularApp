using AngularApp1.Server.DB;
using AngularApp1.Server.Entities;
using AngularApp1.Server.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace AngularApp1.Server.Repositories.ProductRepo
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<bool> IsProductNameUniqueAsync(string productName)
        {
            return await Context.Set<Product>().AnyAsync(p => p.Name == productName);
        }
     
    }

}
