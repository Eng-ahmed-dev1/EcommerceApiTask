namespace Ecomerec.BLL.DTOs
{
    public class CartWriteDTO
    {
        public int UserId { get; set; }
        public ICollection<CartWriteDTO> cartItems = new HashSet<CartWriteDTO>();
    }
}