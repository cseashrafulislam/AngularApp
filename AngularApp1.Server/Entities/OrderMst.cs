using System.Collections.Generic;

namespace AngularApp1.Server.Entities
{
    public class OrderMst
    {
        public OrderMst()
        {
            OrderDtls = new List<OrderDtls>();
        }
        public int Id { get; set; }
        public Customer Customer { get; set; }
        public int CustomerId { get; set; }
        public DateTime OrderDate { get; set; }
        public string Remarks { get; set; }

        public IEnumerable<OrderDtls> OrderDtls { get; set; }

    }
    public class OrderDtls
    {

        public int Id { get; set; }
        public OrderMst OrderMst { get; set; }
        public int OrderMstId { get; set; }

        public Product Product { get; set; }
        public int ProductId { get; set; }

        public decimal Price { get; set; }
        public decimal Qty { get; set; }


    }
}
