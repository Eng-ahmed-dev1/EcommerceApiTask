namespace Ecomerec.BLL.DTOs
{
    public class CartItemWriteDTO
    {
        public int Quantity { get; set; }
        public int CartId { get; set; }
        public int ProductId { get; set; }
    }
}