namespace Ecomerec.BLL.DTOs
{
    public class CartReadDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public decimal TotalPrice { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public ICollection<CartItemReadDTO> cartItems = new HashSet<CartItemReadDTO>();
    }
}