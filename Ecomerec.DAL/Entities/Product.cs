using System.ComponentModel.DataAnnotations.Schema;

namespace Ecomerec.DAL
{
    public class Product
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public int StockQuantity { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        public ICollection<CartItem> CartItems { get; set; } = new HashSet<CartItem>();

    }
}