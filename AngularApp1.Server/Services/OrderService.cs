using AngularApp1.Server.Entities;
using AngularApp1.Server.Migrations;
using AngularApp1.Server.Repositories;
using AngularApp1.Server.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace AngularApp1.Server.Services
{
    public class OrderService
    {
        private readonly IUnitOfWork _unitOfWork;

        public OrderService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // Get a specific order by ID
        public async Task<OrderViewModel> GetOrder(int id)
        {
            var order = await _unitOfWork.Order.FindAsync(id);
            if (order == null) return null;

            return new OrderViewModel
            {
                Id = order.Id,
                OrderDate = order.OrderDate,
                Remarks = order.Remarks,
                OrderDtls = order.OrderDtls.Select(od => new OrderDtlsViewModel
                {
                    Id = od.Id,
                    Price = od.Price,
                    Qty = od.Qty
                }).ToList()
            };
        }

        // Get all orders
        public async Task<IEnumerable<OrderViewModel>> GetOrders()
        {
            var orders = await _unitOfWork.Order.GetAllAsync();
            return orders.Select(order => new OrderViewModel
            {
                Id = order.Id,
                OrderDate = order.OrderDate,
                Remarks = order.Remarks,
                OrderDtls = order.OrderDtls.Select(od => new OrderDtlsViewModel
                {
                    Id = od.Id,
                    Price = od.Price,
                    Qty = od.Qty
                }).ToList()
            }).ToList();
        }

        // Create a new order
        public async Task<OrderViewModel> CreateOrder(OrderViewModel model)
        {
            var order = new OrderMst
            {
                CustomerId = model.CustomerId,
                OrderDate = model.OrderDate,
                Remarks = model.Remarks,
                OrderDtls = model.OrderDtls.Select(od => new OrderDtls
                {
                    ProductId = od.ProductId,
                    Price = od.Price,
                    Qty = od.Qty
                }).ToList()
            };

            await _unitOfWork.Order.AddAsync(order);
            await _unitOfWork.SaveChangesAsync();

            return new OrderViewModel
            {
                Id = order.Id,
                CustomerId = order.CustomerId,
                OrderDate = order.OrderDate,
                Remarks = order.Remarks,
                OrderDtls = order.OrderDtls.Select(od => new OrderDtlsViewModel
                {
                    Id = od.Id,
                    ProductId = od.ProductId,
                    Price = od.Price,
                    Qty = od.Qty
                }).ToList()
            };
        }

        // Update an existing order
        public async Task<int> UpdateOrder(int id, OrderViewModel model)
        {
            var order = await _unitOfWork.Order.FindAsync(id);
            if (order == null) return 0;

            order.CustomerId = model.CustomerId;
            order.OrderDate = model.OrderDate;
            order.Remarks = model.Remarks;
            // Update order details
            // (Handle adding, removing, and updating order details as needed)

            _unitOfWork.Order.Update(order);
            return await _unitOfWork.SaveChangesAsync();
        }

        // Delete an order by ID
        public async Task<int> DeleteOrder(int id)
        {
            var order = await _unitOfWork.Order.FindAsync(id);
            if (order == null) return 0;

            _unitOfWork.Order.Remove(order);
            return await _unitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<OrderDtlsViewModel>> GetOrderDetails(int orderId)
        {
            var order = await _unitOfWork.Order.FindAsync(orderId);
            if (order == null)
            {
                return null; // or throw an exception if you prefer
            }

            return order.OrderDtls.Select(od => new OrderDtlsViewModel
            {
                Id = od.Id,
                ProductId = od.ProductId,
                Price = od.Price,
                Qty = od.Qty,
            }).ToList();
        }
        public async Task<OrderDtlsViewModel> AddOrderDetails(int orderId, OrderDtlsViewModel orderDtlModel)
        {
            var order = await _unitOfWork.Order.FindAsync(orderId);
            if (order == null)
            {
                return null; // or throw an exception
            }

            var orderDtl = new OrderDtls
            {
                OrderMstId = orderId,
                ProductId = orderDtlModel.ProductId,
                Price = orderDtlModel.Price,
                Qty = orderDtlModel.Qty
            };

            await _unitOfWork.OrderDtls.AddAsync(orderDtl);
            await _unitOfWork.SaveChangesAsync();

            return new OrderDtlsViewModel
            {
                Id = orderDtl.Id,
                ProductId = orderDtl.ProductId,
                Price = orderDtl.Price,
                Qty = orderDtl.Qty,
            };
        }
        public async Task<int> UpdateOrderDetails(int orderDtlId, OrderDtlsViewModel orderDtlModel)
        {
            var orderDtl = await _unitOfWork.OrderDtls.FindAsync(orderDtlId);
            if (orderDtl == null)
            {
                return 0; // Order detail not found
            }

            orderDtl.ProductId = orderDtlModel.ProductId;
            orderDtl.Price = orderDtlModel.Price;
            orderDtl.Qty = orderDtlModel.Qty;

            _unitOfWork.OrderDtls.Update(orderDtl);
            return await _unitOfWork.SaveChangesAsync();
        }
        public async Task<int> DeleteOrderDetails(int orderDtlId)
        {
            var orderDtl = await _unitOfWork.OrderDtls.FindAsync(orderDtlId);
            if (orderDtl == null)
            {
                return 0; // Order detail not found
            }

            _unitOfWork.OrderDtls.Remove(orderDtl);
            return await _unitOfWork.SaveChangesAsync();
        }
        public async Task<(IEnumerable<OrderMst> Orders, int TotalCount)> GetOrdersGetPaginatedOrdersAsync(int page, int limit, string customerName = null)
        {
            var orders = await _unitOfWork.Order.GetPaginatedOrdersAsync(page,limit, customerName);
            return (orders.Orders, orders.TotalCount); 
        }

    }


}
