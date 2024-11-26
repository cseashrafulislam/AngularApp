using AngularApp1.Server.Entities;
using AngularApp1.Server.Migrations;
using AngularApp1.Server.Services;
using AngularApp1.Server.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AngularApp1.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly OrderService _orderService;

        public OrderController(OrderService orderService)
        {
            _orderService = orderService;
        }

        // Get an order by ID
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderViewModel>> GetOrder(int id)
        {
            var order = await _orderService.GetOrder(id);
            if (order == null)
            {
                return NotFound();
            }
            return Ok(order);
        }

        // Get all orders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderViewModel>>> GetOrders()
        {
            var orders = await _orderService.GetOrders();
            return Ok(orders);
        }

        // Create a new order
        [HttpPost]
        public async Task<ActionResult<OrderViewModel>> CreateOrder([FromBody] OrderViewModel orderModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var order = await _orderService.CreateOrder(orderModel);
            return CreatedAtAction(nameof(GetOrder), new { id = order.Id }, order);
        }

        // Update an existing order
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrder(int id, [FromBody] OrderViewModel orderModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _orderService.UpdateOrder(id, orderModel);
            if (result > 0)
            {
                return NoContent();
            }

            return NotFound();
        }

        // Delete an order by ID
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var result = await _orderService.DeleteOrder(id);
            if (result > 0)
            {
                return NoContent();
            }
            return NotFound();
        }

        // Get order details by order ID
        [HttpGet("{orderId}/details")]
        public async Task<ActionResult<IEnumerable<OrderDtlsViewModel>>> GetOrderDetails(int orderId)
        {
            var orderDetails = await _orderService.GetOrderDetails(orderId);
            if (orderDetails == null)
            {
                return NotFound();
            }
            return Ok(orderDetails);
        }

        // Add order details to an order
        [HttpPost("{orderId}/details")]
        public async Task<ActionResult<OrderDtlsViewModel>> AddOrderDetails(int orderId, [FromBody] OrderDtlsViewModel orderDtlModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var orderDtl = await _orderService.AddOrderDetails(orderId, orderDtlModel);
            return CreatedAtAction(nameof(GetOrderDetails), new { orderId = orderId }, orderDtl);
        }

        // Update order details
        [HttpPut("details/{orderDtlId}")]
        public async Task<IActionResult> UpdateOrderDetails(int orderDtlId, [FromBody] OrderDtlsViewModel orderDtlModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _orderService.UpdateOrderDetails(orderDtlId, orderDtlModel);
            if (result > 0)
            {
                return NoContent();
            }

            return NotFound();
        }

        // Delete order details
        [HttpDelete("details/{orderDtlId}")]
        public async Task<IActionResult> DeleteOrderDetails(int orderDtlId)
        {
            var result = await _orderService.DeleteOrderDetails(orderDtlId);
            if (result > 0)
            {
                return NoContent();
            }
            return NotFound();
        }

        [HttpGet("GetOrdersWithPage")]
        public async Task<ActionResult<IEnumerable<OrderMst>>> GetOrdersWithPage(int pageNumber = 1, int pageSize = 10)
        {
            var result = await _orderService.GetOrdersGetPaginatedOrdersAsync(pageNumber, pageSize);
            return Ok( result.Orders.ToList());
        }


    }
}
