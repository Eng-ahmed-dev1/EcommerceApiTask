namespace Ecomerec.BLL.DTOs
{
    public class ProductReadDTO
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public int StockQuantity { get; set; }
        public decimal Price { get; set; }
    }
}