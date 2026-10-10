using System.ComponentModel.DataAnnotations.Schema;

namespace Ecomerec.DAL
{
    public class CartItem
    {
        public int Id { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public int CartId { get; set; }
        public Cart Cart { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }

    }
}