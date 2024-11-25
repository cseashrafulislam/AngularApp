using AngularApp1.Server.Entities;
using AngularApp1.Server.Repositories;
using AngularApp1.Server.ViewModels;

namespace AngularApp1.Server.Services
{
    public class CustomerService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CustomerService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Customer> AddCustomer(CustomerViewModel model)
        {
            var Customer = new Customer
            {
                CustomerName = model.CustomerName,
                Address = model.Address,
                PhoneNo = model.PhoneNo,
            };

            await _unitOfWork.Customer.AddAsync(Customer);
            await _unitOfWork.SaveChangesAsync();
            return Customer;
        }

        public async Task<IEnumerable<Customer>> GetList()
        {
            return await _unitOfWork.Customer.GetAllAsync();
        }

        public async Task<Customer> GetCustomer(int id)
        {
            return await _unitOfWork.Customer.FindAsync(id);
        }

        public async Task<int> UpdateCustomer(CustomerViewModel model)
        {
            var Customer = await _unitOfWork.Customer.FindAsync(model.Id);
            if (Customer == null) return 0;

            Customer.CustomerName   = model.CustomerName;
            Customer.Address = model.Address;
            Customer.PhoneNo = model.PhoneNo;
            _unitOfWork.Customer.Update(Customer);
            await _unitOfWork.SaveChangesAsync();
            return 1;
        }

        public async Task<int> DeleteCustomer(int id)
        {
            var Customer = await _unitOfWork.Customer.FindAsync(id);
            if (Customer == null) return 0;

            _unitOfWork.Customer.Remove(Customer);
            await _unitOfWork.SaveChangesAsync();
            return 1;
        }
    }
}
