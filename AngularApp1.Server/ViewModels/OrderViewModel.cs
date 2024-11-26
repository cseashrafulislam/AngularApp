using AngularApp1.Server.Entities;

namespace AngularApp1.Server.ViewModels
{
    public class OrderViewModel
    {
        public OrderViewModel()
        {
            OrderDtls = new List<OrderDtlsViewModel>();
        }
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public DateTime OrderDate { get; set; }
        public string Remarks { get; set; }

        public IEnumerable<OrderDtlsViewModel> OrderDtls { get; set; }

    }
    public class OrderDtlsViewModel
    {

        public int Id { get; set; }
        public int OrderMstId { get; set; }
        public int ProductId { get; set; }
        public decimal Price { get; set; }
        public decimal Qty { get; set; }


    }
}
