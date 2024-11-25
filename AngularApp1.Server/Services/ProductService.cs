using AngularApp1.Server.Entities;
using AngularApp1.Server.Repositories;
using AngularApp1.Server.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AngularApp1.Server.Services
{
    public class ProductService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Product> AddProduct(ProductViewModel model)
        {
            var product = new Product
            {
                Name = model.Name,
                Price = model.Price
            };

            await _unitOfWork.Product.AddAsync(product);
            await _unitOfWork.SaveChangesAsync();
            return product;
        }

        public async Task<IEnumerable<Product>> GetList()
        {
            return await _unitOfWork.Product.GetAllAsync();
        }

        public async Task<Product> GetProduct(int id)
        {
            return await _unitOfWork.Product.FindAsync(id);
        }

        public async Task<int> UpdateProduct(ProductViewModel model)
        {
            var product = await _unitOfWork.Product.FindAsync(model.Id);
            if (product == null) return 0;

            product.Name = model.Name;
            product.Price = model.Price;
            _unitOfWork.Product.Update(product);
            await _unitOfWork.SaveChangesAsync();
            return 1;
        }

        public async Task<int> DeleteProduct(int id)
        {
            var product = await _unitOfWork.Product.FindAsync(id);
            if (product == null) return 0;

            _unitOfWork.Product.Remove(product);
            await _unitOfWork.SaveChangesAsync();
            return 1;
        }
    }
}
